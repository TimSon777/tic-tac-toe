using Microsoft.Extensions.Logging;

namespace Application.Events.Test;

public sealed class TestEventHandler : EventHandlerBase<TestEvent>
{
    private readonly ILogger<TestEventHandler> _logger;

    public TestEventHandler(ILogger<TestEventHandler> logger)
    {
        _logger = logger;
    }

    protected override Task Handle(TestEvent @event)
    {
        var number = @event.Number + Random.Shared.Next(201, 300);
        
        _logger.LogInformation("Event Number: {Number}", number);
        
        return Task.CompletedTask;
    }
}