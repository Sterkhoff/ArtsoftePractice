using Api.Controllers.Question.Responses;

namespace Api.Controllers.Test.Responses;

public record GetTestQuestionsResponse(List<GetQuestionResponse> Questions);