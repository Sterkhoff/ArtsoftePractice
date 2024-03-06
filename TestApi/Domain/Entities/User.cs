using System.ComponentModel.DataAnnotations;
using Core.Dal;
using Domain.Repositories;

namespace Domain.Entities;

/// <summary>
/// соеденил ProfileApi с этим, чтобы хватило сущностей
/// </summary>
public record User
    (Guid Id, [Length(3, 20)]string UserName, [EmailAddress] string Email, List<Test> PassedTests, List<Test> CreatedTests) : BaseEntityDal<Guid>(Id)
{
    public async Task<Guid> SaveUserAsync(IUserRepository userRepository)
    {
        return await userRepository.AddUserAsync(this);
    }

    public async Task AddPassedTest(Test test)
    {
        PassedTests.Add(test);
    }

    public async Task CreateTestAsync(Test test)
    {
        CreatedTests.Add(test);
    }
}