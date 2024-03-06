using Logic.Answer.Models;

namespace Logic.Answer.Interfaces;

public interface IAnswerLogicManager
{
    public Task<Guid> AddAnswerAsync(AnswerLogic answer);
    public Task<List<AnswerLogic>> GetCommentAnswerAsync(Guid commentId);
}