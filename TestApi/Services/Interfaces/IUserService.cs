using Domain.Entities;
using Domain.Interfaces;

namespace Services.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUserAsync(User user);
    public Task<User> GetUserById(Guid id);
}