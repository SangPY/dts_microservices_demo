using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dts_orders_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            return Ok(new[] { "Order 1", "Order 2", "Order 3" });
        }

    }
}
