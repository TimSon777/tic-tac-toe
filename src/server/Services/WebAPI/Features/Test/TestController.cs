using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Features.Test;

[Route("api")]
public sealed class TestController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("random")]
    public Task<IActionResult> Get()
    {
        var number = Random.Shared.Next(101);
        return Task.FromResult<IActionResult>(Ok(number));
    }
}