using Domain.Entities;

namespace Services.Interfaces;

public interface ITestService
{
    public Task<Guid> CreateTestAsync(Test test);
    public Task<List<Question>> GetTestQuestionsAsync(Guid testId);
    public Task<Test> GetTestByIdAsync(Guid testId);
    public Task<Test[]> GetAllAsync();
}