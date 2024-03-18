using Core.RabbitRpcLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.RabbitRpcLogic.Services;

public class RabbitConsumer : IRabbitConsumer
{
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public RabbitConsumer(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("PostExchange", ExchangeType.Direct);
            _queueName = "Post";
            _channel.QueueBind(_queueName, "PostExchange","PostKey");
            _connection.ConnectionShutdown += ConnectionShutdown;
            Console.WriteLine("Connection has been created");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not connect to the rabbitmq: {ex.Message}");
        }
    }
    private void ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("Connection shutdown");
    }
    
    public EventingBasicConsumer CreateConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);
        
        if(_queueName is not null)
        {
            _channel.BasicConsume(_queueName, true, consumer);
        }

        return consumer;
    }
}