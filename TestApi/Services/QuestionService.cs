using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class QuestionService(IStoreQuestion storeQuestion) : IQuestionService
{
    private readonly IStoreQuestion _storeQuestion = storeQuestion;
    
    public async Task<Guid> CreateQuestionAsync(Question question)
    {
        return await question.SaveAsync(_storeQuestion);
    }

    public async Task<List<Question>> GetTestQuestionsAsync(Guid testId)
    { 
        return await _storeQuestion.GetTestQuestionsAsync(testId);
    }

    public async Task<Question> GetQuestionByIdAsync(Guid questionId)
    {
        return await _storeQuestion.GetQuestionById(questionId);
    }
}