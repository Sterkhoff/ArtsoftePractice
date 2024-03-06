using Domain.Interfaces;
using Domain.Entities;
using Services.Interfaces;

namespace Services;

public class TestService(IStoreTest storeTest) : ITestService
{
    private readonly IStoreTest _storeTest = storeTest;
    public async Task<Guid> CreateTestAsync(Test test)
    {
        return await test.SaveAsync(_storeTest);
    }

    public async Task<List<Test>> GetUserPassedTestsAsync(Guid userId)
    {
        return await _storeTest.GetUserPassedTests(userId);
    }

    public async Task<List<Test>> GetUserCreatedTestsAsync(Guid userId)
    {
        return await _storeTest.GetUserCreatedTests(userId);
    }

    public async Task<Test[]> GetAllAsync()
    {
        return await _storeTest.GetAllAsync();
    }

    public async Task<Test> GetTestByIdAsync(Guid testId)
    {
       return await _storeTest.GetTestByIdAsync(testId);
    }
}