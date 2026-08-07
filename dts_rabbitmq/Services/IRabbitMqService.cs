namespace dts_rabbitmq.Services
{
    public interface IRabbitMqService
    {
        void PublishOrder(string order);
    }
}
