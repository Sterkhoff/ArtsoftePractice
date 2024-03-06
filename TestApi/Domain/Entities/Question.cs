using Core.Dal;
using Domain.Interfaces;

namespace Domain.Entities;

public record Question(Guid Id, Guid TestId, string QuestionText, List<string> Answers, string RightAnswer)
    : BaseEntityDal<Guid>(Id)
{
    public async Task<Guid> SaveAsync(IStoreQuestion storeQuestion)
    {
        return await storeQuestion.AddQuestionAsync(this);
    }
}