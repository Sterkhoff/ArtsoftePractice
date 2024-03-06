using Api.Controllers.Test.Responses;
using Api.Controllers.User.Requests;
using Api.Controllers.User.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers.User;

[Route("user")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [Route("get_by_id")]
    [HttpGet]
    [ProducesResponseType<GetUserResponse>(200)]
    public async Task<IActionResult> GetUserAsync([FromQuery] Guid userId)
    {
        var user = _mapper.Map<GetUserResponse>(await _userService.GetUserByIdAsync(userId));
        return Ok(user);
    }

    [Route("create")]
    [HttpPost]
    [ProducesResponseType<CreateUserResponse>(200)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest userRequest)
    {
        var user = _mapper.Map<Domain.Entities.User>(userRequest);
        await _userService.CreateUserAsync(user);
        return Ok(_mapper.Map<CreateUserResponse>(user));
    }
    
    [Route("get_user_created_tests")]
    [HttpGet]
    [ProducesResponseType<GetUserCreatedTestsResponse>(200)]
    public async Task<IActionResult> GetUserCreatedTestsAsync([FromQuery] Guid userId)
    {
        var tests = await _userService.GetUserCreatedTestsAsync(userId);
        var createdTests = _mapper.Map<GetUserCreatedTestsResponse>(tests);
        return Ok(createdTests);
    }
    
    [Route("get_user_passed_tests")]
    [HttpGet]
    [ProducesResponseType<GetUserPassedTestsResponse>(200)]
    public async Task<IActionResult> GetUserPassedTestsAsync([FromQuery] Guid userId)
    {
        var tests = await _userService.GetUserPassedTestsAsync(userId);
        var passedTests = _mapper.Map<GetUserPassedTestsResponse>(tests);
        return Ok(passedTests);
    }
}