using Domain.Enums;

namespace Application.Queries.CurrentGame;

public sealed class CurrentGameQueryResult
{
    public required PlayerSign Sign { get; set; }
    public required string MateUserName { get; set; }
    public required GameStatus GameStatus { get; set; }
    public string[][] Board { get; set; } = default!;
}