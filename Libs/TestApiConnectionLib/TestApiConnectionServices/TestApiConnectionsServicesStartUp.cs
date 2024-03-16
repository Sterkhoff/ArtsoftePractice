using Microsoft.Extensions.DependencyInjection;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace TestApiConnectionLib.TestApiConnectionServices;

public static class TestApiConnectionsServicesStartUp
{
    public static IServiceCollection AddTestApiConnectionServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITestApiConnectionService, TestApiConnectionService>();
        return serviceCollection;
    }
}