namespace Application.Commands.StopGame;

public sealed class StopGameCommand : ICommand<StopGameCommandResult>
{
    public required string UserName { get; set; }
}