namespace Core.Connection.RabbitRpcLogic.Services.Interfaces;

internal interface IRabbitProducer
{
    public Task<string> SendMessage(string message, string replyQueueName, string replyQueueRoutingKey,
        string publishRoutingKey, CancellationToken cancellationToken = default);
}