using Domain.Entities;
using Domain.Interfaces;

namespace Services.Interfaces;

public interface ITestService
{
    public Task<Guid> CreateTestAsync(Test test);
    public Task<List<Test>> GetUserPassedTestsAsync(Guid userId);
    public Task<List<Test>> GetUserCreatedTestsAsync(Guid userId);
    public Task<Test> GetTestByIdAsync(Guid testId);
    public Task<Test[]> GetAllAsync();
}