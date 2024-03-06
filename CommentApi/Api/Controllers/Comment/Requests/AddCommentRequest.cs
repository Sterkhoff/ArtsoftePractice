namespace Api.Controllers.Comment.Requests;

public record AddCommentRequest(Guid UserId, Guid TestId, string Text);