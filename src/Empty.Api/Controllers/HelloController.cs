using Microsoft.AspNetCore.Mvc;

namespace Empty.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : Controller
    {
        private readonly TimeProvider _timeProvider;

        public HelloController(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        [HttpGet("time")]
        public IActionResult Get()
        {
            return new OkObjectResult($"Hello, World! The time is {_timeProvider.GetUtcNow()}.");
        }
    }
}