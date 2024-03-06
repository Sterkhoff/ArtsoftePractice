using Api.Controllers.Question.Requests;
using Api.Controllers.Question.Responses;
using AutoMapper;
using Domain.Entities;

namespace Api.AutoMapper.QuestionMappingProfile;

public class QuestionMappingProfile : Profile
{
    public QuestionMappingProfile()
    {
        CreateMap<CreateQuestionRequest, Question>()
            .ConstructUsing(x => new Question(new Guid(), x.TestId, x.QuestionText, x.Answers, x.RightAnswer));
        
        CreateMap<List<Question>, GetTestQuestionsResponse>()
            .ConstructUsing(x => 
                new GetTestQuestionsResponse(x.Select(z => new GetQuestionResponse(z.Id, z.TestId, z.QuestionText, z.Answers, z.RightAnswer)).ToList()));

        CreateMap<Question, GetQuestionResponse>();
        CreateMap<GetQuestionResponse, Question>();
    }
}