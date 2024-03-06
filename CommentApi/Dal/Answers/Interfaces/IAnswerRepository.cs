using Dal.Answers.Models;
using Dal.Comments.Models;

namespace Dal.Answers.Interfaces;

public interface IAnswerRepository
{
    public Task<Guid> AddAnswerAsync(AnswerDal answer);
    public Task<List<AnswerDal>> GetCommentAnswersAsync(Guid commentId);
}