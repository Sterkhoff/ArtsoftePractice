using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApiConnectionLib.TestApiConnectionServices.Connection;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;
using TestApiConnectionLib.TestApiConnectionServices.Rabbit.Consumer;
using TestApiConnectionLib.TestApiConnectionServices.RequestServices;

namespace TestApiConnectionLib.TestApiConnectionServices;

public static class TestApiConnectionsServicesStartUp
{
    public static IServiceCollection AddTestApiConnectionServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        if (configuration.GetSection("RequestsType").Value == "http")
            serviceCollection.AddScoped<ITestApiRequestService, TestApiRequestServiceHttp>();
        else
            serviceCollection.AddScoped<ITestApiRequestService, TestApiRequestServiceRabbit>();
        
        serviceCollection.AddScoped<ITestApiConnectionService, TestApiConnectionService>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddConsumer<TReceiveHandler>(this IServiceCollection serviceCollection) where TReceiveHandler : class, ITestApiReceiveHandler
    {
        serviceCollection.AddScoped<ITestApiReceiveHandler, TReceiveHandler>();
        serviceCollection.AddHostedService<TestApiRabbitConsumer>();
        return serviceCollection;
    }
}