using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Connection.RabbitRpcLogic.Services.Interfaces;

public interface IRabbitService
{
    public Task<string> SendMessage(string message, string replyQueueName, string replyRoutingKey,
        string publishRoutingKey);

    public EventingBasicConsumer CreateConsumer(Func<string, Task<string>> onReceiveFunc, 
            string queueName, string routingKey, IModel channel);
}