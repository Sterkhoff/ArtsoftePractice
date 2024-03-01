using Logic.Comment.Models;

namespace Logic.Comment.Interfaces;

public interface ICommentLogicManager
{
    public Task<List<CommentLogic>> GetTestCommentsAsync(Guid testId);
    public Task<Guid> AddComment(CommentLogic comment);
}