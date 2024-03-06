using Api.Controllers.Test.Requests;
using Api.Controllers.Test.Responses;
using Api.Controllers.User.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers.Test;

[Route("test")]
public class TestController(ITestService testService, IMapper mapper) : ControllerBase
{
    private readonly ITestService _testService = testService;
    private readonly IMapper _mapper = mapper;
    
    [Route("create")]
    [ProducesResponseType<CreateTestResponse>(200)]
    public async Task<IActionResult> CreateTestAsync(CreateTestRequest testRequest)
    {
        var test = _mapper.Map<Domain.Entities.Test>(testRequest);
        await _testService.CreateTestAsync(test);
        return Ok(_mapper.Map<CreateTestResponse>(test));
    }

    [Route("get_by_id")]
    [ProducesResponseType<GetTestResponse>(200)]
    public async Task<IActionResult> GetTestByIdAsync(Guid testId)
    {
        var test = await _testService.GetTestByIdAsync(testId);
        var testResponse = _mapper.Map<CreateTestResponse>(test);
        return Ok(testResponse);
    }

    [Route("get_user_created_tests")]
    [ProducesResponseType<GetUserCreatedTestsResponse>(200)]
    public async Task<IActionResult> GetUserCreatedTestsAsync(Guid userId)
    {
        var tests = await _testService.GetUserCreatedTestsAsync(userId);
        var createdTests = _mapper.Map<GetUserCreatedTestsResponse>(tests);
        return Ok(createdTests);
    }
    
    [Route("get_user_passed_tests")]
    [ProducesResponseType<GetUserPassedTestsResponse>(200)]
    public async Task<IActionResult> GetUserPassedTestsAsync(Guid userId)
    {
        var tests = await _testService.GetUserPassedTestsAsync(userId);
        var passedTests = _mapper.Map<GetUserPassedTestsResponse>(tests);
        return Ok(passedTests);
    }
}