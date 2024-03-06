using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data;

public class QuestionRepository : IQuestionRepository
{
    private readonly ConcurrentDictionary<Guid, Question> _questionsData = new ();
    public async Task<Guid> AddQuestionAsync(Question question)
    {
        _questionsData[question.Id] = question;
        return question.Id;
    }

    public async Task<Question> GetQuestionById(Guid questionId)
    {
        if (_questionsData.TryGetValue(questionId, out var question))
            return question;
        throw new Exception("Вопрос не найден");
    }
}