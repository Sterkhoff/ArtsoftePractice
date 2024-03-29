using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;

namespace TestApiConnectionLib.TestApiConnectionServices.Interfaces;

public interface ITestApiRequestService
{
    public Task<CheckTestExistTestApiResponse> CheckTestExistAsync(CheckTestExistTestApiRequest testApiRequest);
}