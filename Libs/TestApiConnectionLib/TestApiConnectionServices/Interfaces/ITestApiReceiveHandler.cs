namespace TestApiConnectionLib.TestApiConnectionServices.Interfaces;

public interface ITestApiReceiveHandler
{
    public Task<string> CheckTestExist(string testId);
}