using Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Events;

public sealed class TestEventHandler : IConsumer<TestEvent>
{
    private readonly ILogger<TestEventHandler> _logger;

    public TestEventHandler(ILogger<TestEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TestEvent> context)
    {
        _logger.LogInformation("From event handler: {Number}", context.Message.Number);
        return Task.CompletedTask;
    }
}