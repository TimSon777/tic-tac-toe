namespace Application.Commands.StartGame;

public sealed class StartGameCommandResult
{
    public required bool IsStart { get; set; }
    public string? InitiatorUserName { get; set; }
}