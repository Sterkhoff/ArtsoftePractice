using AutoMapper;
using Logic.Answer.Interfaces;
using Dal.Answers.Interfaces;
using Dal.Answers.Models;
using Dal.Comments.Models;
using Logic.Answer.Models;

namespace Logic.Answer;

public class AnswerLogicManager(IAnswerRepository answerRepository, IMapper mapper) : IAnswerLogicManager
{
    private readonly IAnswerRepository _answerRepository = answerRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Guid> AddAnswerAsync(AnswerLogic answer)
    {
        var answerDal = _mapper.Map<AnswerDal>(answer);
        return await _answerRepository.AddAnswerAsync(answerDal);
    }

    public async Task<List<AnswerLogic>> GetCommentAnswerAsync(Guid commentId)
    {
        var answers = await _answerRepository.GetCommentAnswersAsync(commentId);
        var answersLogic = _mapper.Map<List<AnswerLogic>>(answers);
        return answersLogic;
    }
}