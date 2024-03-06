using Api.Controllers.Question.Responses;
using Api.Controllers.Test.Responses;
using Api.Controllers.User.Requests;
using Api.Controllers.User.Responses;
using AutoMapper;
using Domain.Entities;

namespace Api.AutoMapper.UserMappingProfile;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, GetUserResponse>()
            .ConstructUsing(user => new GetUserResponse(user.Id, user.UserName, user.Email, 
                new GetUserPassedTestsResponse(user.PassedTests.Select(test => new GetTestResponse(test.Id, test.CreatorId, test.Title, 
                    new GetTestQuestionsResponse(test.Questions.Select(question => new GetQuestionResponse(question.Id, question.TestId, question.QuestionText, question.Answers, question.RightAnswer)).ToList()))).ToList()), 
                new GetUserCreatedTestsResponse(user.CreatedTests.Select(test => new GetTestResponse(test.Id, test.CreatorId, test.Title, 
                    new GetTestQuestionsResponse(test.Questions.Select(question => new GetQuestionResponse(question.Id, question.TestId, question.QuestionText, question.Answers, question.RightAnswer)).ToList()))).ToList())));
        CreateMap<User, CreateUserResponse>();
        CreateMap<CreateUserRequest, User>()
            .ConstructUsing(createReq => new User(new Guid(), createReq.Username, createReq.Email, 
                new List<Test>(), new List<Test>()));
    }
}