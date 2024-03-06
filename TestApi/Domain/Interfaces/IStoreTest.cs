using Domain.Entities;

namespace Domain.Interfaces;

public interface IStoreTest
{
    public Task<Guid> AddTestAsync(Test test);
    public Task<Test[]> GetAllAsync();
    public Task<Test> GetTestByIdAsync(Guid testId);
    public Task<List<Test>> GetUserPassedTests(Guid userId);
    public Task<List<Test>> GetUserCreatedTests(Guid userId);
}