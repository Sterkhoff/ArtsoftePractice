using Api.Controllers.Test.Responses;

namespace Api.Controllers.User.Responses;

public record GetUserResponse(Guid Id, string UserName, string Email, 
    GetUserPassedTestsResponse PassedTests, GetUserCreatedTestsResponse CreatedTests);