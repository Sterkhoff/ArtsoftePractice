using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;

public static class InfrastructureStartUp
{
    public static IServiceCollection TryAddInfrastructure(this IServiceCollection services)
    {
        services.TryAddScoped<IQuestionRepository, QuestionRepository>();
        services.TryAddScoped<ITestRepository, TestRepository>();
        services.TryAddScoped<IUserRepository, UserRepository>();
        return services;
    }
}