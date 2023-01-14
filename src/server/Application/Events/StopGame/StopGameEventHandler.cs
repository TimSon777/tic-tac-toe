using Application.Abstractions.Repositories;

namespace Application.Events.StopGame;

public sealed class StopGameEventHandler : EventHandlerBase<StopGameEvent>
{
    private readonly IGameRepository _gameRepository;

    public StopGameEventHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    protected override async Task Handle(StopGameEvent @event)
    {
        var game = await _gameRepository.GetGameByIdAsync(@event.GameId);
        game.Status = @event.GameStatus;
        await _gameRepository.CommitAsync();
    }
}