using Dal.Answers;
using Dal.Answers.Interfaces;
using Dal.Comments;
using Dal.Comments.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dal;

public static class DalStartUp
{
    public static IServiceCollection TryAddDal(this IServiceCollection services)
    {
        services.TryAddScoped<ICommentRepository, CommentRepository>();
        services.TryAddScoped<IAnswerRepository, AnswerRepository>();
        return services;
    }
}