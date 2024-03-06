using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data;

public class TestRepository : ITestRepository
{
    private readonly ConcurrentDictionary<Guid, Test> _testsData = new();
    public async Task<Guid> AddTestAsync(Test test)
    {
        _testsData[test.Id] = test;
        return test.Id;
    }

    public async Task<Test[]> GetAllAsync()
    {
        return _testsData.Values.ToArray();
    }

    public async Task<Test> GetTestByIdAsync(Guid testId)
    {
        if (_testsData.TryGetValue(testId, out var test))
            return test;
        throw new Exception("Тест не найден");
    }
}