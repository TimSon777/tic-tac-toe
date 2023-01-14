namespace WebAPI.Features.CurrentGame;

public sealed class Response
{
    public required char Sign { get; set; }
    public required string MateUserName { get; set; }
    public required string GameStatus { get; set; }
    public char[][] Board { get; set; } = default!;
}