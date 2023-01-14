namespace Application.Commands.StopGame;

public sealed class StopGameCommandResult
{
    public required string GameStatus { get; set; } = default!;
    public required string MateUserName { get; set; }
}