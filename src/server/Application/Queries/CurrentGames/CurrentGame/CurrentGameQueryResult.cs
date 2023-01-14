using Domain.Enums;

namespace Application.Queries.CurrentGames.CurrentGame;

public sealed class CurrentGameQueryResult
{
    public required PlayerSign Sign { get; set; }
    public required string MateUserName { get; set; }
    public required GameStatus GameStatus { get; set; }
    public char[][] Board { get; set; } = default!;
}