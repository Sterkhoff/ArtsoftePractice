using Api.Controllers.Test.Responses;

namespace Api.Controllers.User.Responses;

public record GetUserCreatedTestsResponse(List<GetTestResponse> CreatedTests);