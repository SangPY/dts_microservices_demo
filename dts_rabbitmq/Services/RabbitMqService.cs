using dts_rabbitmq.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using IModel = RabbitMQ.Client.IModel;

namespace dts_rabbitmq.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IModel _channel;

        public RabbitMqService(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqSettings.Value.HostName,
                Port = int.Parse(rabbitMqSettings.Value.Port),
                UserName = rabbitMqSettings.Value.UserName,
                Password = rabbitMqSettings.Value.Password
            };

            var connection = factory.CreateConnection();

            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "orders_exchange", type: ExchangeType.Direct, durable: true,
                autoDelete: false);

            _channel.QueueDeclare(queue: "orders_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: "orders_queue", exchange: "orders_exchange", routingKey: "order_routing_key");
        }

        public void PublishOrder(string order)
        {
            var body = System.Text.Encoding.UTF8.GetBytes(order);

            _channel.BasicPublish(exchange: "orders_exchange", routingKey: "order_routing_key", basicProperties: null, body: body);
        }
    }
}
