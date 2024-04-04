using System.Text;
using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Connection.RabbitRpcLogic.Services;

internal class RabbitConsumerFactory : IRabbitConsumerFactory
{
    public EventingBasicConsumer CreateConsumer(Func<string, Task<string>> onReceiveFunc, 
        string queueName, string routingKey, IModel channel)
    {
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, "rpc_exc", routingKey);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            string response = string.Empty;

            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                response = await onReceiveFunc(message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                response = string.Empty;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(exchange: "rpc_exc",
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
            }
        };
        channel.BasicConsume(queueName, true, consumer);
        return consumer;
    }
}