using Logic.Answer.Models;

namespace Api.Controllers.Answer.Responses;

public struct GetCommentAnswersResponse(Guid CommentId, List<AnswerLogic> answers);