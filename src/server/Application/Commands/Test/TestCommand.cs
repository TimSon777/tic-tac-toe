namespace Application.Commands.Test;

public sealed class TestCommand : ICommand<TestCommandResult>
{
    public required int Number { get; set; }
}