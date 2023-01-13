using Application.Queries.CurrentGames.Models;
using Domain.Entities;
using Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace Application.Queries.CurrentGames;

[Mapper]
public static partial class CurrentGamesMapper
{
    public static CurrentGamesQueryResult Map(this IEnumerable<Game> games, int totalCount, bool hasMore)
    {
        return new CurrentGamesQueryResult
        {
            Games = games.Select(g => new GameItem
            {
                Id = g.Id,
                UserName = g.Initiator.User.UserName!,
                CreatedDateTimeUtc = g.CreatedDateTimeUtc,
                IsAvailableToJoin = g.Status == GameStatus.NotStarted
            }),
            HasMore = hasMore,
            TotalCount = totalCount
        };
    }
}