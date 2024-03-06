using Domain.Entities;
using Domain.Repositories;
using Services.Interfaces;

namespace Services;

internal class UserService(IUserRepository userRepository, ITestRepository testRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITestRepository _testRepository = testRepository;
    
    public async Task<Guid> CreateUserAsync(User user)
    {
        return await user.SaveUserAsync(_userRepository);
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task PassTestAsync(Guid testId, Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var test = await _testRepository.GetTestByIdAsync(testId);
        await user.AddPassedTest(test);
    }

    public async Task<List<Test>> GetUserPassedTestsAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user.PassedTests;
    }

    public async Task<List<Test>> GetUserCreatedTestsAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user.CreatedTests;
    }
}