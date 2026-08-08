using dts_rabbitmq.Models;
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
        public async Task Send()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5671,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

             channel.QueueDeclare(queue: "SangQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            List<EmailMessage> emails = new();

            emails = new List<EmailMessage>
            {
                new EmailMessage { To = "dothanhsang95@gmail.com",Subject = "Test Subject", Body = "Test Body" },
                new EmailMessage { To = "dothanhsangpy@gmail.com", Subject = "Test Subject 2", Body = "Test Body 2" },
                new EmailMessage { To = "sangdtpy95@gmail.com", Subject = "Test Subject 3", Body = "Test Body 3" },
            };

            var message = System.Text.Json.JsonSerializer.Serialize(emails);
            var body = System.Text.Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "SangExchange", routingKey: "TestKey", body: body);

            //for (int i = 0; i < 100; i++)
            //{
            //    var message = $"Message {i}";
            //    var body = System.Text.Encoding.UTF8.GetBytes(message);
            //    channel.BasicPublish(exchange: "SangExchange", routingKey: "TestKey", body: body);
            //    Console.WriteLine($" [x] Sent {message}");
            //}
        }

        [HttpGet]
        public async Task Receive()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5671,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "SangQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
                await Task.Yield();
            };
            channel.BasicConsume(queue: "SangQueue", autoAck: true, consumer: consumer);
            // Keep the application running to listen for messages
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
