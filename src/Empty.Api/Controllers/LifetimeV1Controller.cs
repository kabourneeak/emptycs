using Microsoft.AspNetCore.Mvc;

namespace Empty.Api.Controllers;

[ApiController]
[Route("api/lifetime/v1")]
public class LifetimeV1Controller : ControllerBase
{
    private readonly IHostApplicationLifetime lifetime;

    public LifetimeV1Controller(IHostApplicationLifetime lifetime)
    {
        this.lifetime = lifetime;
    }

    /// <summary>
    /// Gracefully shutdown the API Server.
    /// </summary>
    [HttpPost("stop")]
    public IActionResult Stop()
    {
        lifetime.StopApplication();
        return new OkResult();
    }
}
