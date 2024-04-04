using Core.Connection.RabbitRpcLogic;
using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using TestApiConnectionLib.TestApiConnectionServices.Interfaces;

namespace TestApiConnectionLib.TestApiConnectionServices.Rabbit.Consumer;

public class TestApiRabbitConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScope;
    private readonly IModel _channel;

    public TestApiRabbitConsumer(IServiceScopeFactory rabbitService, RabbitConnectionPool connectionPool)
    {
        var connectionPool1 = connectionPool;
        _serviceScope = rabbitService;
        var connection = connectionPool1.Get();
        _channel = connection.CreateModel();
        connectionPool1.Return(connection);
        _channel.ExchangeDeclare("rpc_exc", ExchangeType.Direct);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScope.CreateScope();
        var receiveHandler = scope.ServiceProvider.GetRequiredService<ITestApiReceiveHandler>();
        var rabbitService = scope.ServiceProvider.GetRequiredService<IRabbitService>();
        rabbitService.CreateConsumer(receiveHandler.CheckTestExist, "check_test_exist",
            "check_test_exist", _channel);
        return Task.CompletedTask;
    }
}