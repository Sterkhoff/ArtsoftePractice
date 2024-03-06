using Api.Controllers.Comment.Requests;
using Api.Controllers.Comment.Responses;
using AutoMapper;
using Logic.Comment.Models;

namespace Api.AutoMapper;

public class LogicToApiMappingProfile : Profile
{
    public LogicToApiMappingProfile()
    {
        CreateMap<CommentLogic, GetTestCommentsResponse>();
        CreateMap<AddCommentRequest, CommentLogic>();
    }
}