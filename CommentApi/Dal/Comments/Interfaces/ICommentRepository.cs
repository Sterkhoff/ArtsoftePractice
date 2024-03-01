using System.Collections.Concurrent;
using Dal.Comments.Models;

namespace Dal.Comments.Interfaces;

public interface ICommentRepository
{
    public Task<List<CommentDal>> GetTestCommentsAsync(Guid testId);
    public Task<Guid> AddComment(CommentDal comment);
}