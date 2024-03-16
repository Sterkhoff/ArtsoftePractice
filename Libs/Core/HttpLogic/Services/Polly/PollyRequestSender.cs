using Polly;

namespace Core.HttpLogic.Services.Polly;

public class PollyRequestSender : IPollyRequestSender
{
    public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient, 
        CancellationToken cancellationToken, PollyData configData, 
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var res = await Policy
            .Handle<Exception>()
            .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(configData.SleepDurationInSeconds), configData.OnRetryFunc)
            .ExecuteAsync(async () => await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken));

        return res;
    }
}