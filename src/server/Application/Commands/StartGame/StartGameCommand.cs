namespace Application.Commands.StartGame;

public sealed class StartGameCommand : ICommand<StartGameCommandResult>
{
    public required string UserName { get; set; }
}