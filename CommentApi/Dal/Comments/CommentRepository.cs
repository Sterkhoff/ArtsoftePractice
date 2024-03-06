using System.Collections.Concurrent;
using Dal.Comments.Interfaces;
using Dal.Comments.Models;

namespace Dal.Comments;

internal class CommentRepository : ICommentRepository
{
    private readonly ConcurrentDictionary<Guid, List<CommentDal>> _testsCommentsData = new();
    
    public async Task<List<CommentDal>> GetTestCommentsAsync(Guid testId)
    {
        if (_testsCommentsData.TryGetValue(testId, out var comments))
            return comments;
        throw new Exception("Тест не найден");
    }

    public async Task<Guid> AddComment(CommentDal comment)
    {
        if (_testsCommentsData.TryGetValue(comment.TestId, out var value))
            value.Add(comment);
        else
            _testsCommentsData[comment.TestId] = new List<CommentDal> { comment };
        return comment.Id;
    }
}