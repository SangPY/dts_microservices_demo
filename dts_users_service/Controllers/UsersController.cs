using Microsoft.AspNetCore.Mvc;

namespace dts_users_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(new[] { "User 1", "User 2", "User 3" });
        }
    }
}
