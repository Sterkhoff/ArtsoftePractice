namespace Api.Controllers.Question.Responses;

public record GetQuestionResponse(Guid Id, Guid TestId, string QuestionText, List<string> Answers, string RightAnswer);