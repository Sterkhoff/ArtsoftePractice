using Api.Controllers.Test.Requests;

namespace Api.Controllers.User.Requests;

public record CreateUserRequest(string Username, string Email);