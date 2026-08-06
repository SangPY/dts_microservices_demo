using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dts_rabbitmq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        [HttpPost]
        public async Task Send() { 
            var factory = new ConnectionFactory() {
                HostName = "localhost",
                Port = 5671,
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "SangQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            for (int i = 0; i < 100; i++)
            {
                var message = $"Message {i}";
                var body = System.Text.Encoding.UTF8.GetBytes(message);
                await channel.BasicPublishAsync(exchange: "SangExchange", routingKey: "TestKey", body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
        }

        [HttpGet]
        public async Task Receive()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5671,
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "SangQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
                await Task.Yield();
            };
            await channel.BasicConsumeAsync(queue: "SangQueue", autoAck: true, consumer: consumer);
            // Keep the application running to listen for messages
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
