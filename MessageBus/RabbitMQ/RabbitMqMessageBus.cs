using APIGestaoPedidos.MessageBus.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace APIGestaoPedidos.MessageBus.RabbitMQ
{
    public class RabbitMqMessageBus : IMessageBus
    {
        public Task PublicarAsync<T>(T mensagem, string queueName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mensagem));
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
