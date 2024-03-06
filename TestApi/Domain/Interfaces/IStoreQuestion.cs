using Domain.Entities;

namespace Domain.Interfaces;

public interface IStoreQuestion
{
    public Task<Guid> AddQuestionAsync(Question question);
    public Task<List<Question>> GetTestQuestionsAsync(Guid testId);
    public Task<Question> GetQuestionById(Guid questionId);
}