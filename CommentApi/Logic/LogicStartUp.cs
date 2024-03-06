using Logic.Answer;
using Logic.Answer.Interfaces;
using Logic.Comment;
using Logic.Comment.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Logic;

public static class LogicStartUp
{
    public static IServiceCollection TryAddLogic(this IServiceCollection services)
    {
        services.TryAddScoped<IAnswerLogicManager, AnswerLogicManager>();
        services.TryAddScoped<ICommentLogicManager, CommentLogicManager>();
        return services;
    }
}