using Api.Controllers.Question.Responses;
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
    [HttpPost]
    [ProducesResponseType<CreateTestResponse>(200)]
    public async Task<IActionResult> CreateTestAsync([FromBody] CreateTestRequest testRequest)
    {
        var test = _mapper.Map<Domain.Entities.Test>(testRequest);
        await _testService.CreateTestAsync(test);
        return Ok(_mapper.Map<CreateTestResponse>(test));
    }

    [Route("get_by_id")]
    [HttpGet]
    [ProducesResponseType<GetTestResponse>(200)]
    public async Task<IActionResult> GetTestByIdAsync([FromQuery] Guid testId)
    {
        var test = await _testService.GetTestByIdAsync(testId);
        var testResponse = _mapper.Map<CreateTestResponse>(test);
        return Ok(testResponse);
    }
    
    [Route("get_test_questions")]
    [HttpGet]
    [ProducesResponseType<GetTestQuestionsResponse>(200)]
    public async Task<IActionResult> GetTestQuestionsAsync([FromQuery] Guid testId)
    {
        var questions = await _testService.GetTestQuestionsAsync(testId);
        return Ok(_mapper.Map<GetTestQuestionsResponse>(questions));
    }
}