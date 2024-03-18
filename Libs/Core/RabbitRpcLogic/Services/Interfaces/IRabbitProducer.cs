namespace Core.RabbitRpcLogic.Services.Interfaces;

public interface IRabbitProducer
{
    public bool SendMessage<T>(T message);
}