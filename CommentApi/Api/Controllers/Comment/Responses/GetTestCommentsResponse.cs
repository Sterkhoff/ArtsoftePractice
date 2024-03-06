using Logic.Comment.Models;

namespace Api.Controllers.Comment.Responses;

public struct GetTestCommentsResponse(Guid TestId, List<CommentLogic> Comments);