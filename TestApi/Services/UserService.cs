using Domain.Entities;
using Domain.Interfaces;

namespace Services.Interfaces;

public class UserService(IStoreUser storeUser, IStoreTest storeTest) : IUserService
{
    private readonly IStoreUser _storeUser = storeUser;
    private readonly IStoreTest _storeTest = storeTest;
    public async Task<Guid> CreateUserAsync(User user)
    {
        return await user.SaveUserAsync(storeUser);
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _storeUser.GetUserByIdAsync(id);
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _storeUser.GetUserByIdAsync(userId);
    }

    public async Task UserPassTest(Guid testId, Guid userId)
    {
        var user = await _storeUser.GetUserByIdAsync(userId);
        var test = await _storeTest.GetTestByIdAsync(testId);
        await user.PassTest(test);
    }
}