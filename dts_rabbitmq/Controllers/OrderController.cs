using dts_rabbitmq.Models;
using dts_rabbitmq.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dts_rabbitmq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IRabbitMqService _rabbitMqService;

        public OrderController(IRabbitMqService rabbitMqService, ApplicationDbContext applicationDbContext)
        {
            _rabbitMqService = rabbitMqService;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _applicationDbContext.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            order!.CreatedAt = DateTime.UtcNow;
            // Serialize the order object to JSON
            var orderJson = System.Text.Json.JsonSerializer.Serialize(order);
            // Publish the order to RabbitMQ
            _rabbitMqService.PublishOrder(orderJson);
            return Ok(new { message = "Order created and published to RabbitMQ." });
        }
    }
}
