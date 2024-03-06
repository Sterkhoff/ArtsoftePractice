using AutoMapper;
using Dal.Comments.Interfaces;
using Dal.Comments.Models;
using Logic.Comment.Interfaces;
using Logic.Comment.Models;

namespace Logic.Comment;

public class CommentLogicManager(ICommentRepository commentRepository, IMapper mapper) : ICommentLogicManager
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<List<CommentLogic>> GetTestCommentsAsync(Guid testId)
    {
        return _mapper.Map<List<CommentLogic>>(await _commentRepository.GetTestCommentsAsync(testId));
    }

    public async Task<Guid> AddComment(CommentLogic comment)
    {
        var commentDal = _mapper.Map<CommentDal>(comment);
        return await _commentRepository.AddComment(commentDal);
    }
}