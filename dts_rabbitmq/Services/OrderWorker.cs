using dts_rabbitmq.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Threading.RateLimiting;
using IModel = RabbitMQ.Client.IModel;

namespace dts_rabbitmq.Services
{
    public class OrderWorker : BackgroundService
    {
        private readonly IModel _channel;

        private readonly IServiceScopeFactory _scopeFactory;

        public OrderWorker(IServiceScopeFactory scopeFactory,
            IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _scopeFactory = scopeFactory;

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqSettings.Value.HostName,
                UserName = rabbitMqSettings.Value.UserName,
                Password = rabbitMqSettings.Value.Password,
                Port = int.Parse(rabbitMqSettings.Value.Port)
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "orders_queue", durable: true,
                exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                var order = System.Text.Json.JsonSerializer.Deserialize<Order>(message);

                Console.WriteLine($"Received order: {order.Id}, {order.ProductName}, {order.Price}, {order.CreatedAt}");

                await ProcessOrder(order!);
            };

            _channel.BasicConsume("orders_queue", true, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(Order order)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                // Simulate some processing time
                await Task.Delay(1000);
                // Save the order to the database
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine($"Processed order: {order.Id}, {order.ProductName}, {order.Price}, {order.CreatedAt} and saved to database.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _channel.Dispose();
            await base.StopAsync(cancellationToken);
        }
    }
}
