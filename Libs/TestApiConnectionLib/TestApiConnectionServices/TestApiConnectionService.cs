using Core.HttpLogic.Services;
using Core.HttpLogic.Services.Connection;
using Microsoft.Extensions.Configuration;
using Core.HttpLogic.Services.Interfaces;
using Core.HttpLogic.Services.Polly;
using Core.HttpLogic.Services.Request;
using Microsoft.Extensions.DependencyInjection;
using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace TestApiConnectionLib.TestApiConnectionServices;

internal class TestApiConnectionService : ITestApiConnectionService
{
    private readonly IHttpRequestService _httpRequestService;

    public TestApiConnectionService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        if (configuration.GetSection("Connection").Value == "http")
        {
            _httpRequestService = serviceProvider.GetRequiredService<IHttpRequestService>();
        }
        else
        {
            // RPC по rabbit
        }
    }
    public async Task<CheckTestExistTestApiResponse> CheckTestExistAsync(CheckTestExistTestApiRequest testApiRequest)
    {
        var httpRequestData = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri("http://localhost:5031/test/get_by_id"),
            UsePolly = true,
            RequestPollyData = new PollyData(5, (exception, retryCount, _) =>
            {
                Console.WriteLine($"{retryCount} попытка повтора, ошибка: {exception}");
                return Task.CompletedTask;
            }),
            QueryParameterList = new List<KeyValuePair<string, string>>
            {
                new ("testId", testApiRequest.TestId.ToString())
            }
        };
        var connectionData = new HttpConnectionData
        {
            ClientName = "asdf"
        };
        var response = await _httpRequestService
            .SendRequestAsync<CheckTestExistTestApiResponse>(httpRequestData, connectionData);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Test not found");
        return response.Body;
    }
}