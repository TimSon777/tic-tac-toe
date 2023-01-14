namespace Application.Queries.CurrentGames.CurrentGame;

public sealed class CurrentGameQuery : IQuery<CurrentGameQueryResult>
{
    public required string UserName { get; set; }
}