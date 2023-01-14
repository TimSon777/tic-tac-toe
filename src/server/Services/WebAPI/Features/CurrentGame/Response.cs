namespace WebAPI.Features.CurrentGame;

public sealed class Response
{
    public required string Sign { get; set; }
    public required string MateUserName { get; set; }
    public required string GameStatus { get; set; }
    public string[][] Board { get; set; } = default!;
}