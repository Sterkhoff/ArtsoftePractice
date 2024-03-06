namespace Api.Controllers.Question.Requests;

public record CreateQuestionRequest(Guid TestId, string QuestionText, List<string> Answers, string RightAnswer);