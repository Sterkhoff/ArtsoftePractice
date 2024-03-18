using System.Text;
using Core.RabbitRpcLogic.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.RabbitRpcLogic.Services;

public class RabbitRpcService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitRpcService()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "rpc_queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }

    public EventingBasicConsumer CreateConsumer<TResponse, TRequest>()
    {
        var consumer = new EventingBasicConsumer(_channel);
        _channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);
        consumer.Received += (model, ea) =>
        { 
/'o            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                int n = int.Parse(message);
                Console.WriteLine($" [.] Fib({message})");
                response = Fib(n).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine($" [.] {e.Message}");
                response = string.Empty;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(exchange: string.Empty,
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        };
        return consumer;
    }
}