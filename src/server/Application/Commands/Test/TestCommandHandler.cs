using Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Test;

public sealed class TestCommandHandler : IConsumer<TestCommand>
{
    private readonly ILogger<TestCommandHandler> _logger;
    private readonly IApplicationMediator _applicationMediator;

    public TestCommandHandler(ILogger<TestCommandHandler> logger, IApplicationMediator applicationMediator)
    {
        _logger = logger;
        _applicationMediator = applicationMediator;
    }

    public async Task Consume(ConsumeContext<TestCommand> context)
    {
        var number = context.Message.Number + Random.Shared.Next(100);
        
        var result = new TestCommandResult
        {
            Number = context.Message.Number + Random.Shared.Next(100)
        };

        var @event = new TestEvent
        {
            Number = number
        };

        await context.RespondAsync(result);
        await _applicationMediator.Event(@event);

        _logger.LogInformation("New Number: {Number}", number);
    }
}