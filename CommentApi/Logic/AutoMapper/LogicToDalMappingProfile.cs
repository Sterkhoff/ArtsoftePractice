using AutoMapper;
using Dal.Answers.Models;
using Dal.Comments.Models;
using Logic.Answer.Models;
using Logic.Comment.Models;

namespace Logic.AutoMapper;

public class LogicToDalMappingProfile : Profile
{
    public LogicToDalMappingProfile()
    {
        CreateMap<AnswerLogic, AnswerDal>()
            .ConstructUsing(x => new AnswerDal(new Guid(), x.CommentId, x.UserId, x.Text));

        CreateMap<CommentLogic, CommentDal>()
            .ConstructUsing(x => new CommentDal(new Guid(), x.UserId, x.TestId, x.Text));
        
        CreateMap<CommentDal, CommentLogic>();
        CreateMap<AnswerDal, AnswerLogic>();
    }
}