using Core.Dal;
using Domain.Interfaces;

namespace Domain.Entities;

public record Test(Guid Id, Guid CreatorId, string Title, List<Question> Questions) : BaseEntityDal<Guid>(Id)
{
    public async Task<Guid> SaveAsync(IStoreTest storeTest)
    {
        return await storeTest.AddTestAsync(this);
    }
};