using Api.Controllers.Comment.Requests;
using Api.Controllers.Comment.Responses;
using AutoMapper;
using Logic.Comment.Interfaces;
using Logic.Comment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Comment;

[Route("[controller]/[action]")]
public class CommentController(ICommentLogicManager commentLogicManager, IMapper mapper) : ControllerBase
{
    private readonly ICommentLogicManager _commentLogicManager = commentLogicManager;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [ProducesResponseType<List<GetTestCommentsResponse>>(200)]
    public async Task<IActionResult> GetTestCommentsAsync([FromQuery] Guid testId)
    {
        var commentsResponses = await _commentLogicManager.GetTestCommentsAsync(testId);
        return Ok(_mapper.Map<List<GetTestCommentsResponse>>(commentsResponses));
    }

    [HttpPost]
    [ProducesResponseType<AddCommentResponse>(200)]
    public async Task<IActionResult> AddCommentAsync([FromBody] AddCommentRequest comment)
    {
        var commentLogic = _mapper.Map<CommentLogic>(comment);
        var id = await _commentLogicManager.AddComment(commentLogic);
        return Ok(new AddCommentResponse(id));
    }
}