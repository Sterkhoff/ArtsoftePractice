using Api.Controllers.Question.Responses;

namespace Api.Controllers.Test.Responses;

public record GetTestResponse(Guid Id, Guid CreatorId, string Title, GetTestQuestionsResponse Questions);