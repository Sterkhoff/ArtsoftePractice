using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.RabbitRpcLogic.Services.Interfaces;

public interface IRabbitConsumer
{
    public EventingBasicConsumer CreateConsumer();
}