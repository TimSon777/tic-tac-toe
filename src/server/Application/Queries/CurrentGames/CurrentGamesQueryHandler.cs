using Application.Abstractions.Repositories;

namespace Application.Queries.CurrentGames;

public sealed class CurrentGamesQueryHandler : QueryHandlerBase<CurrentGamesQuery, CurrentGamesQueryResult>
{
    private readonly IGameRepository _gameRepository;

    public CurrentGamesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    protected override async Task<CurrentGamesQueryResult> Handle(CurrentGamesQuery command)
    {
        var games = await _gameRepository.GetCurrentGamesAsync(command.ItemsCount, command.PageNumber);
        var count = await _gameRepository.CountCurrentGamesAsync();
        var hasMore = count - command.PageNumber * command.ItemsCount > 0;
        return games.Map(count, hasMore);
    }
}