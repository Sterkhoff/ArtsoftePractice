using Core.TraceLogic.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Services.Interfaces;

namespace Services;

internal class TestService(ITestRepository testRepository, IUserRepository userRepository, IEnumerable<ITraceReader> traceReader) 
    : ITestService
{
    private readonly ITestRepository _testRepository = testRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEnumerable<ITraceReader> _traceReader = traceReader;
    public async Task<Guid> CreateTestAsync(Test test)
    {
        var creator = await _userRepository.GetUserByIdAsync(test.CreatorId);
        await creator.CreateTestAsync(test);
        return await test.SaveAsync(_testRepository);
    }

    public async Task<List<Question>> GetTestQuestionsAsync(Guid testId)
    {
        var test = await _testRepository.GetTestByIdAsync(testId);
        return test.Questions;
    }

    public async Task<Test[]> GetAllAsync()
    {
        return await _testRepository.GetAllAsync();
    }

    public async Task<Test> GetTestByIdAsync(Guid testId)
    {
        
        return await _testRepository.GetTestByIdAsync(testId);
    }
}