using Api.Controllers.Question.Requests;
using Api.Controllers.Question.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers.Question;

[Route("question")]
public class QuestionController(IMapper mapper, IQuestionService questionService) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuestionService _questionService = questionService;

    
    [Route("create")]
    [HttpPost]
    [ProducesResponseType<CreateQuestionResponse>(200)]
    public async Task<IActionResult> CreateQuestionAsync([FromBody] CreateQuestionRequest questionRequest)
    {
        var question = _mapper.Map<Domain.Entities.Question>(questionRequest);
        await _questionService.CreateQuestionAsync(question);
        return Ok(_mapper.Map<CreateQuestionResponse>(question));
    }

    [Route("get_by_id")]
    [HttpGet]
    [ProducesResponseType<GetQuestionResponse>(200)]
    public async Task<IActionResult> GetQuestionById([FromQuery] Guid questionId)
    {
        var question = await _questionService.GetQuestionByIdAsync(questionId);
        return Ok(_mapper.Map<GetQuestionResponse>(question));
    }
}