namespace Application.Queries.CurrentGames;

public sealed class CurrentGamesQuery : IQuery<CurrentGamesQueryResult>
{
    public required int ItemsCount { get; set; }
    public required int PageNumber { get; set; }
}