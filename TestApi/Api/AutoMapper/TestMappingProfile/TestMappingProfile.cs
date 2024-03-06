using Api.Controllers.Question.Responses;
using Api.Controllers.Test.Requests;
using Api.Controllers.Test.Responses;
using AutoMapper;
using Domain.Entities;

namespace Api.AutoMapper.TestMappingProfile;

public class TestMappingProfile : Profile
{
    public TestMappingProfile()
    {
        CreateMap<Test, GetTestResponse>()
            .ForMember(dest => dest.Questions, 
                src => 
                    src.MapFrom(opt => opt.Questions
                        .Select(x => new GetQuestionResponse(x.Id, x.TestId, x.QuestionText, x.Answers, x.RightAnswer)).ToList()));

        CreateMap<List<Test>, GetUserCreatedTestsResponse>()
            .ForMember(dest => dest.CreatedTests,
                src =>
                    src.MapFrom(opt => opt.Select(x => new GetTestResponse(x.Id, x.CreatorId, x.Title,
                        new GetTestQuestionsResponse(x.Questions.Select(z =>
                            new GetQuestionResponse(z.Id, z.TestId, z.QuestionText, z.Answers, z.RightAnswer)).ToList())))));

        CreateMap<List<Test>, GetUserPassedTestsResponse>()
            .ForMember(dest => dest.PassedTests,
                src =>
                    src.MapFrom(opt => opt.Select(x => new GetTestResponse(x.Id, x.CreatorId, x.Title,
                        new GetTestQuestionsResponse(x.Questions.Select(z =>
                                new GetQuestionResponse(z.Id, z.TestId, z.QuestionText, z.Answers, z.RightAnswer))
                            .ToList())))));
        CreateMap<Test, CreateTestResponse>();
        
        CreateMap<CreateTestRequest, Test>()
            .ConstructUsing(x => new Test(new Guid(), x.CreatorId, x.Title,
                x.Questions.Select(questReq => new Question(new Guid(), questReq.TestId, questReq.QuestionText,
                    questReq.Answers, questReq.RightAnswer)).ToList()));
    }
}