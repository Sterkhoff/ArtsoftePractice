using Api.Controllers.Question;
using Api.Controllers.Question.Requests;
using Api.Controllers.Question.Responses;

namespace Api.Controllers.Test.Requests;

public record CreateTestRequest(Guid CreatorId, string Title, List<CreateQuestionRequest> Questions);