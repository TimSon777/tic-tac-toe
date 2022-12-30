using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Features.Test;

[Route("api")]
public sealed class TestController : ControllerBase
{
    [HttpGet("random")]
    public Task<IActionResult> Get()
    {
        var number = Random.Shared.Next(101);
        return Task.FromResult<IActionResult>(Ok(number));
    }
}