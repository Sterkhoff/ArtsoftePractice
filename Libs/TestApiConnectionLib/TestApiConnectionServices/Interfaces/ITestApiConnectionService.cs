using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;

namespace TestApiConnectionLib.TestApiConnectionServices.Interfaces;

public interface ITestApiConnectionService
{
    public Task<CheckTestExistTestApiResponse> CheckTestExistAsync(CheckTestExistTestApiRequest testTestApiRequest);
}