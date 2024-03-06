using Domain.Entities;

namespace Domain.Repositories;

public interface ITestRepository
{
    public Task<Guid> AddTestAsync(Test test);
    public Task<Test[]> GetAllAsync();
    public Task<Test> GetTestByIdAsync(Guid testId);
}