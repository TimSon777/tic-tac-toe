namespace Application.Commands.CreateGame;

public sealed class CreateGameCommand : ICommand<CreateGameCommandResult>
{
    public required string UserName { get; set; }
}