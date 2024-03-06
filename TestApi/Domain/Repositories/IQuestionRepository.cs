using Domain.Entities;

namespace Domain.Repositories;

public interface IQuestionRepository
{
    public Task<Guid> AddQuestionAsync(Question question);
    public Task<Question> GetQuestionById(Guid questionId);
}