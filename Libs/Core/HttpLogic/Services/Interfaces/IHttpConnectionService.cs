using Core.HttpLogic.Services.Connection;
using Core.HttpLogic.Services.Polly;

namespace Core.HttpLogic.Services.Interfaces;

internal interface IHttpConnectionService
{
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

    /// <summary>
    /// Отправляет запрос без использования Polly
    /// </summary>
    public Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient, 
        CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
    
    
    /// <summary>
    /// Отправляет запрос с использованием Polly
    /// </summary>
    public Task<HttpResponseMessage> SendRequestWithPollyAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient,
        CancellationToken cancellationToken, PollyData pollyConfigData,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}