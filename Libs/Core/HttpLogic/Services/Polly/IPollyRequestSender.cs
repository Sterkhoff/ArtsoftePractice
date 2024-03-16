namespace Core.HttpLogic.Services.Polly;

public interface IPollyRequestSender
{
    public Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient, 
        CancellationToken cancellationToken, PollyData configData, 
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}