using Empty.Sdk.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Empty.Api.Controllers;

[ApiController]
[Route("api/ping/v1")]
public class PingV1Controller : ControllerBase
{
    private readonly TimeProvider _timeProvider;

    public PingV1Controller(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    [HttpGet("time")]
    [ProducesResponseType<TimeDto>(StatusCodes.Status200OK)]
    public Task<IActionResult> Time()
    {
        var result = new OkObjectResult(new TimeDto { Time = _timeProvider.GetUtcNow() });

        return Task.FromResult<IActionResult>(result);
    }
}