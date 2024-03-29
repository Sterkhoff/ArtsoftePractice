using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace TestApiConnectionLib.TestApiConnectionServices;

internal class TestApiConnectionService : ITestApiConnectionService
{
    private readonly ITestApiRequestService _requestService;

    public TestApiConnectionService(ITestApiRequestService requestService)
    {
        _requestService = requestService;
    }

    public Task<CheckTestExistTestApiResponse> CheckTestExistAsync(CheckTestExistTestApiRequest testTestApiRequest)
    {
        return _requestService.CheckTestExistAsync(testTestApiRequest);
    }
}