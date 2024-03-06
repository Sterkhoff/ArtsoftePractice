using Api.Controllers.Test.Responses;

namespace Api.Controllers.User.Responses;

public record GetUserPassedTestsResponse(List<GetTestResponse> PassedTests);