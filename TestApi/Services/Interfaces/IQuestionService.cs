using Domain.Entities;

namespace Services.Interfaces;

public interface IQuestionService
{
    public Task<Guid> CreateQuestionAsync(Question question);
    public Task<List<Question>> GetTestQuestionsAsync(Guid testId);
    public Task<Question> GetQuestionByIdAsync(Guid questionId);
}