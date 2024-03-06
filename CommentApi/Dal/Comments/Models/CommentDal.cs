using Core.Dal;

namespace Dal.Comments.Models;

public record CommentDal(Guid Id, Guid UserId, Guid TestId, string Text) 
    : BaseEntityDal<Guid>(Id);