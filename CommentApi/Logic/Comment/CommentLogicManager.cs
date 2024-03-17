using AutoMapper;
using Dal.Comments.Interfaces;
using Dal.Comments.Models;
using Logic.Comment.Interfaces;
using Logic.Comment.Models;
using TestApiConnectionLib.TestApiConnectionServices.DtoModels.CheckTestExist;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace Logic.Comment;

public class CommentLogicManager(ICommentRepository commentRepository, 
    IMapper mapper, ITestApiConnectionService testApiConnectionService) : ICommentLogicManager
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ITestApiConnectionService _testApiConnectionService = testApiConnectionService;
    
    public async Task<List<CommentLogic>> GetTestCommentsAsync(Guid testId)
    {
        await CheckTestExistAsync(testId);
        return _mapper.Map<List<CommentLogic>>(await _commentRepository.GetTestCommentsAsync(testId));
    }

    public async Task<Guid> AddComment(CommentLogic comment)
    {
        await CheckTestExistAsync(comment.TestId);
        var commentDal = _mapper.Map<CommentDal>(comment);
        return await _commentRepository.AddComment(commentDal);
    }

    /// <summary>
    /// отправляет запрос к сервису TestApi, в случае, если теста с айдишником testId не существует, кидает эксепшен
    /// </summary>
    private async Task CheckTestExistAsync(Guid testId)
    {
        await _testApiConnectionService.CheckTestExistAsync(new CheckTestExistTestApiRequest(testId));
    }
}