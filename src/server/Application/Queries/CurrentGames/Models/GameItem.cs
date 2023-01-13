namespace Application.Queries.CurrentGames.Models;

public sealed class GameItem
{
    public required string UserName { get; set; }
    public required int Id { get; set; }
    public required DateTime CreatedDateTimeUtc { get; set; }
    public required bool IsAvailableToJoin { get; set; }
}