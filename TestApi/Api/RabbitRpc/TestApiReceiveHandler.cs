using Newtonsoft.Json;
using Services.Interfaces;
using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace Api.RabbitRpc;

public class TestApiReceiveHandler : ITestApiReceiveHandler
{
    private readonly ITestService _testService;

    public TestApiReceiveHandler(ITestService testService)
    {
        _testService = testService;
    }

    public async Task<string> CheckTestExist(string testId)
    {
        var checkTestReq = JsonConvert.DeserializeObject<CheckTestExistTestApiRequest>(testId);
        var response = await _testService.GetTestByIdAsync(checkTestReq.TestId);
        return JsonConvert.SerializeObject(new CheckTestExistTestApiResponse(response.Id));
    }
}