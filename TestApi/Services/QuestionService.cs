using Domain.Entities;
using Domain.Repositories;
using Services.Interfaces;

namespace Services;

internal class QuestionService(IQuestionRepository questionRepository, ITestRepository testRepository) : IQuestionService
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    private readonly ITestRepository _testRepository = testRepository;
    
    public async Task<Guid> CreateQuestionAsync(Question question)
    {
        var test = await _testRepository.GetTestByIdAsync(question.TestId);
        await test.AddQuestionAsync(question);
        return await question.SaveAsync(_questionRepository);
    }

    public async Task<Question> GetQuestionByIdAsync(Guid questionId)
    {
        return await _questionRepository.GetQuestionById(questionId);
    }
}