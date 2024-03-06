using Core.Dal;
using Domain.Repositories;

namespace Domain.Entities;

public record Question(Guid Id, Guid TestId, string QuestionText, List<string> Answers, string RightAnswer)
    : BaseEntityDal<Guid>(Id)
{
    public async Task<Guid> SaveAsync(IQuestionRepository questionRepository)
    {
        return await questionRepository.AddQuestionAsync(this);
    }
}