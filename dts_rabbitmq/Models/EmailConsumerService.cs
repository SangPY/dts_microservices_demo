using MailKit.Net.Smtp;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dts_rabbitmq.Models
{
    public class EmailConsumerService : BackgroundService
    {
        private IConnection _connection;
        private IModel _model;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5671,
            };

            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(queue: "SangQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_model);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                var emails = System.Text.Json.JsonSerializer.Deserialize<List<EmailMessage>>(message);

                await SendEmail(emails);

                _model.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _model.BasicConsume(queue: "SangQueue", autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task SendEmail(List<EmailMessage> emails)
        {
            foreach (var email in emails)
            {
                // Implementation for sending email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("App", "sangdothanh95@gmail.com"));
                message.To.Add(new MailboxAddress("", email.To));
                message.Subject = email.Subject;
                message.Body = new TextPart("plain") { Text = email.Body };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("sangdothanh95@gmail.com", "your_password");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _model?.Close();
            _connection?.Close();
            return base.StopAsync(cancellationToken);
        }
    }
}
