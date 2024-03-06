using Core.Dal;
using Domain.Repositories;

namespace Domain.Entities;

public record Test(Guid Id, Guid CreatorId, string Title, List<Question> Questions) : BaseEntityDal<Guid>(Id)
{
    public async Task<Guid> SaveAsync(ITestRepository testRepository)
    {
        return await testRepository.AddTestAsync(this);
    }

    public async Task AddQuestionAsync(Question question)
    {
        Questions.Add(question);
    }
};