using Core.Dal;

namespace Dal.Comments.Models;

public record AnswerDal(Guid Id, Guid CommentId, Guid UserId, string Text) 
    : BaseEntityDal<Guid>(Id);