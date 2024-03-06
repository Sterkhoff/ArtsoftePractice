using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data;

public class TestRepository(IStoreUser storeUser) : IStoreTest
{
    private readonly ConcurrentDictionary<Guid, Test> _testsData = new();
    private readonly IStoreUser _storeUser = storeUser;
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

    public async Task<List<Test>> GetUserPassedTests(Guid userId)
    {
        return (await _storeUser.GetUserByIdAsync(userId)).PassedTests;
    }

    public async Task<List<Test>> GetUserCreatedTests(Guid userId)
    {
        return (await _storeUser.GetUserByIdAsync(userId)).CreatedTests;
    }
}