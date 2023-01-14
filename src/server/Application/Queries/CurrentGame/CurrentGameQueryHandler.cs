using Application.Abstractions.Repositories;

namespace Application.Queries.CurrentGame;

public sealed class CurrentGameQueryHandler : QueryHandlerBase<CurrentGameQuery, CurrentGameQueryResult>
{
    private readonly IGameRepository _gameRepository;

    public CurrentGameQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    protected override async Task<CurrentGameQueryResult> Handle(CurrentGameQuery command)
    {
        var game = await _gameRepository.GetActiveGameByInitiatorUserNameAsync(command.UserName);

        return game.Map(command.UserName);
    }
}