using Core.Connection.HttpLogic.Services.Connection;
using Core.Connection.HttpLogic.Services.Interfaces;
using Core.Connection.HttpLogic.Services.Polly;
using Core.Connection.HttpLogic.Services.Request;
using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace TestApiConnectionLib.TestApiConnectionServices.RequestServices;

public class TestApiRequestServiceHttp : ITestApiRequestService
{
    private readonly IHttpRequestService _httpRequestService;

    public TestApiRequestServiceHttp(IHttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
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