using Application.Events.Test;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Test;

public sealed class TestCommandHandler : CommandHandlerBase<TestCommand, TestCommandResult>
{
    private readonly ILogger<TestCommandHandler> _logger;
    private readonly IApplicationMediator _applicationMediator;

    public TestCommandHandler(ILogger<TestCommandHandler> logger, IApplicationMediator applicationMediator)
    {
        _logger = logger;
        _applicationMediator = applicationMediator;
    }

    protected override async Task<TestCommandResult> Handle(TestCommand command)
    {
        var number = command.Number + Random.Shared.Next(1, 100);
        
        var result = new TestCommandResult
        {
            Number = number
        };

        var @event = new TestEvent
        {
            Number = number
        };
        
        await _applicationMediator.Event(@event);

        _logger.LogInformation("Command Number: {Number}", number);

        return result;
    }
}