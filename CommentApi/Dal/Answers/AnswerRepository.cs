using System.Collections.Concurrent;
using Dal.Answers.Interfaces;
using Dal.Comments.Models;

namespace Dal.Answers;

public class AnswerRepository : IAnswerRepository
{
    private readonly ConcurrentDictionary<Guid, List<AnswerDal>> _answersData = new();
    
    public async Task<Guid> AddAnswerAsync(AnswerDal answer)
    {
        if (_answersData.TryGetValue(answer.CommentId, out var answers))
            answers.Add(answer);
        else
            _answersData[answer.CommentId] = new List<AnswerDal> { answer };
        return answer.Id;
    }

    public async Task<List<AnswerDal>> GetCommentAnswersAsync(Guid commentId)
    {
        if (_answersData.TryGetValue(commentId, out var answers))
            return answers;
        throw new Exception("Комментарий не найден");
    }
}