namespace Api.Controllers.Answer.Requests;

public record AddAnswerRequest(Guid UserId, Guid CommentId, string Text);