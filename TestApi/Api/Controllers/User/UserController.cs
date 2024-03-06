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
    [ProducesResponseType<GetUserResponse>(200)]
    public async Task<IActionResult> GetUserAsync(Guid userId)
    {
        var user = _mapper.Map<GetUserResponse>(await _userService.GetUserById(userId));
        return Ok(user);
    }

    [Route("create")]
    [ProducesResponseType<CreateUserResponse>(200)]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest userRequest)
    {
        var user = _mapper.Map<Domain.Entities.User>(userRequest);
        await _userService.CreateUserAsync(user);
        return Ok(_mapper.Map<CreateUserResponse>(user));
    }
}