using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Connection.RabbitRpcLogic.Services;

internal class RabbitService : IRabbitService
{
    private readonly IRabbitConsumerFactory _rabbitConsumerFactory;
    private readonly IRabbitProducer _rabbitProducer;

    public RabbitService(IRabbitProducer rabbitProducer, IRabbitConsumerFactory rabbitConsumer)
    {
        _rabbitConsumerFactory = rabbitConsumer;
        _rabbitProducer = rabbitProducer;
    }

    public async Task<string> SendMessage(string message, string replyQueueName, string replyRoutingKey,
        string publishRoutingKey)
    {
        return await _rabbitProducer.SendMessage(message, replyQueueName, replyRoutingKey, publishRoutingKey);
    }

    public EventingBasicConsumer CreateConsumer(Func<string, Task<string>> onReceiveFunc, 
        string queueName, string routingKey, IModel channel)
    {
        return _rabbitConsumerFactory.CreateConsumer(onReceiveFunc, queueName, routingKey, channel);
    }
}