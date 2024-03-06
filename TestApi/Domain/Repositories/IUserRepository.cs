using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    public Task<Guid> AddUserAsync(User user);
    public Task<User> GetUserByIdAsync(Guid id);
}