using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using Newtonsoft.Json;
using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace TestApiConnectionLib.TestApiConnectionServices.Connection;

public class TestApiRequestServiceRabbit : ITestApiRequestService
{
    private readonly IRabbitService _rabbitService;

    public TestApiRequestServiceRabbit(IRabbitService rabbitProducer)
    {
        _rabbitService = rabbitProducer;
    }
    
    public async Task<CheckTestExistTestApiResponse> CheckTestExistAsync(CheckTestExistTestApiRequest testApiRequest)
    {
        var responseJson = await _rabbitService.SendMessage(JsonConvert.SerializeObject(testApiRequest), 
            "check_test_reply", "check_test_reply", "check_test_exist");
        if (string.IsNullOrEmpty(responseJson))
            throw new Exception("Тест не найден");
        return JsonConvert.DeserializeObject<CheckTestExistTestApiResponse>(responseJson);
    }
}