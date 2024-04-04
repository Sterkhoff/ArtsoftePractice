using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Core.Connection.RabbitRpcLogic;

public class RabbitConnectionPool
{
    private readonly ConcurrentBag<IConnection> _connections;
    private readonly ConnectionFactory _connectionFactory;

    public RabbitConnectionPool()
    {
        _connections = new ConcurrentBag<IConnection>();
        _connectionFactory = new ConnectionFactory { HostName = "localhost", Port = 5672};
    }

    public IConnection Get()
    {
        return _connections.TryTake(out var connection) ? connection : _connectionFactory.CreateConnection();
    }

    public bool Return(IConnection connection)
    {
        _connections.Add(connection);
        return true;
    }
}
