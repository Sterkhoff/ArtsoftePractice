using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data;

public class QuestionRepository(IStoreTest testStore) : IStoreQuestion
{
    private readonly IStoreTest _testStore = testStore;
    private readonly ConcurrentDictionary<Guid, Question> _questionsData = new ();
    public async Task<Guid> AddQuestionAsync(Question question)
    {
        var test = await _testStore.GetTestByIdAsync(question.TestId);
        test.Questions.Add(question);
        _questionsData[question.Id] = question;
        return question.Id;
    }

    public async Task<List<Question>> GetTestQuestionsAsync(Guid testId)
    {
        return (await _testStore.GetTestByIdAsync(testId)).Questions;
    }

    public async Task<Question> GetQuestionById(Guid questionId)
    {
        if (_questionsData.TryGetValue(questionId, out var question))
            return question;
        throw new Exception("Вопрос не найден");
    }
}