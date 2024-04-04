using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Connection.RabbitRpcLogic.Services.Interfaces;

internal interface IRabbitConsumerFactory
{
    public EventingBasicConsumer CreateConsumer(Func<string, Task<string>> onReceiveFunc, 
        string queueName, string routingKey, IModel channel);
}