using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Connection.RabbitRpcLogic.Services;

public static class RabbitServiceStartUp
{
    public static IServiceCollection AddRabbitService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRabbitProducer, RabbitProducer>();
        serviceCollection.AddScoped<IRabbitConsumerFactory, RabbitConsumerFactory>();
        serviceCollection.AddScoped<IRabbitService, RabbitService>();
        serviceCollection.AddSingleton<RabbitConnectionPool>();
        return serviceCollection;
    }
}