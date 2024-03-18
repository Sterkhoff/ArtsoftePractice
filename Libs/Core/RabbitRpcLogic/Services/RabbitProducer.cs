using System.Text;
using Core.RabbitRpcLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Core.RabbitRpcLogic.Services;

public class RabbitProducer : IRabbitProducer
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitProducer(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["Host"],
            Port = int.Parse(_configuration["Port"])
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("PostExchange", ExchangeType.Direct);
            _channel.QueueDeclare("Post", false, false, false, null);
            _channel.QueueBind("Post", "PostExchange", "PostKey", null);
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
        Console.WriteLine("Connection has been shutdown");
    }
    
    public bool SendMessage<T>(T message)
    {
        try
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            _channel.BasicPublish("PostExchange", "PostKey", null, body);
                
            return true;
        }
        catch 
        {
            return false;
        }
    }
}