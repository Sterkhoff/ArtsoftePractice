using Core.HttpLogic.Services.Interfaces;
using Core.HttpLogic.Services.Polly;

namespace Core.HttpLogic.Services.Connection;

internal class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IPollyRequestSender _pollyRequestSender;

    public HttpConnectionService(IHttpClientFactory clientFactory, IPollyRequestSender pollyRequestSender)
    {
        _httpClientFactory = clientFactory;
        _pollyRequestSender = pollyRequestSender;
    }
    public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
    {
        var httpClient = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(httpConnectionData.ClientName);
            
        if (httpConnectionData.Timeout != null)
        {
            httpClient.Timeout = httpConnectionData.Timeout.Value;
        }
        
        return httpClient;
    }
    
    public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var response = await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
        return response;
    }

    public async Task<HttpResponseMessage> SendRequestWithPollyAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient, 
        CancellationToken cancellationToken, PollyData pollyConfigData, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var response = await _pollyRequestSender.SendRequestAsync(httpRequestMessage, httpClient, 
            cancellationToken, pollyConfigData, httpCompletionOption);
        return response;
    }
}