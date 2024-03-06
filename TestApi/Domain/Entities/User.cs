using System.ComponentModel.DataAnnotations;
using Core.Dal;
using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// соеденил ProfileApi с этим, чтобы хватило сущностей
/// </summary>
public record User
    (Guid Id, [Length(3, 20)]string UserName, [EmailAddress] string Email, List<Test> PassedTests, List<Test> CreatedTests) : BaseEntityDal<Guid>(Id)
{
    public async Task<Guid> SaveUserAsync(IStoreUser storeUser)
    {
        return await storeUser.AddUserAsync(this);
    }

    public async Task PassTest(Test test)
    {
        PassedTests.Add(test);
    }

    public async Task CreateTest(Test test)
    {
        CreatedTests.Add(test);
    }
}