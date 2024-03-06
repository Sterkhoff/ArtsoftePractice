using Domain.Entities;

namespace Services.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUserAsync(User user);
    public Task<User> GetUserByIdAsync(Guid id);
    public Task<List<Test>> GetUserPassedTestsAsync(Guid userId);
    public Task<List<Test>> GetUserCreatedTestsAsync(Guid userId);
}