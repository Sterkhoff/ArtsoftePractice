using Core.Dal;

namespace Dal.Answers.Models;

public record AnswerDal(Guid Id, Guid CommentId, Guid UserId, string Text) 
    : BaseEntityDal<Guid>(Id);