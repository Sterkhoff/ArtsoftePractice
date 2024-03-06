using Domain.Entities;

namespace Domain.Interfaces;

public interface IStoreUser
{
    public Task<Guid> AddUserAsync(User user);
    public Task<User> GetUserByIdAsync(Guid id);
}