using Api.Controllers.Answer.Requests;
using Api.Controllers.Answer.Responses;
using AutoMapper;
using Logic.Answer;
using Logic.Answer.Interfaces;
using Logic.Answer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Answer;

[Route("[controller]/[action]")]
public class AnswerController(IAnswerLogicManager answerLogicManager, IMapper mapper) : ControllerBase
{
    private readonly IAnswerLogicManager _answerLogicManager = answerLogicManager;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [ProducesResponseType<AddAnswerResponse>(200)]
    public async Task<IActionResult> AddAnswerAsync([FromBody] AddAnswerRequest answer)
    {
        var answerLogic = _mapper.Map<AnswerLogic>(answer);
        var id = await _answerLogicManager.AddAnswerAsync(answerLogic);
        return Ok(id);
    }

    [HttpGet]
    [ProducesResponseType<GetCommentAnswersResponse>(200)]
    public async Task<IActionResult> GetCommentAnswerAsync([FromQuery] Guid commentId)
    {
        var answers = await _answerLogicManager.GetCommentAnswerAsync(commentId);
        return Ok(new GetCommentAnswersResponse(commentId, answers));
    }
}