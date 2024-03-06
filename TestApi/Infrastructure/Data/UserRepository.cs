using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _usersData = new();
    public async Task<Guid> AddUserAsync(User user)
    {
        _usersData[user.Id] = user;
        return user.Id;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        if (_usersData.TryGetValue(id, out var user))
            return user;
        throw new Exception("Пользователь не найден");
    }
}