using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Interfaces;

namespace Services;

public static class ServicesStartUp
{
    public static IServiceCollection TryAddServices(this IServiceCollection services)
    {
        services.TryAddScoped<IUserService, UserService>();
        services.TryAddScoped<IQuestionService, QuestionService>();
        services.TryAddScoped<ITestService, TestService>();
        return services;
    }
}