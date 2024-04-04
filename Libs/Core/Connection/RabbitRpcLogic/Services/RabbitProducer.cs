using System.Collections.Concurrent;
using System.Text;
using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Connection.RabbitRpcLogic.Services;

internal class RabbitProducer : IDisposable, IRabbitProducer
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly HashSet<string> _queues = [];
    private readonly RabbitConnectionPool _connectionPool;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _callbackMapper = new();

    public RabbitProducer(RabbitConnectionPool connectionPool)
    {
        _connectionPool = connectionPool;
        _connection = connectionPool.Get();
        _channel = _connection.CreateModel();
    }

    private void CreateAndListenReplyQueue(string queueName, string queueRoutingKey)
    {
        _channel.ExchangeDeclare("rpc_exc", ExchangeType.Direct);
        _channel.QueueDeclare(queueName, false, false, false, null);
        _channel.QueueBind(queueName, "rpc_exc", queueRoutingKey, null);
        _queues.Add(queueName);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        };

        _channel.BasicConsume(consumer: consumer,
            queue: queueName,
            autoAck: true);
    }

    public Task<string> SendMessage(string message, string replyQueueName, string replyQueueRoutingKey, 
        string publishRoutingKey, CancellationToken cancellationToken = default)
    {
        if (!_queues.Contains(replyQueueName))
            CreateAndListenReplyQueue(replyQueueName, replyQueueRoutingKey);
        IBasicProperties props = _channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueueName;
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var tcs = new TaskCompletionSource<string>();
        _callbackMapper.TryAdd(correlationId, tcs);

        _channel.BasicPublish("rpc_exc", publishRoutingKey, props, messageBytes);

        cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));
        return tcs.Task;
    }

    public void Dispose()
    {
        _channel.Close();
        _connectionPool.Return(_connection);
    }
}