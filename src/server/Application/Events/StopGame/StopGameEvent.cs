using Domain.Enums;

namespace Application.Events.StopGame;

public sealed class StopGameEvent : IEvent
{
    public required int GameId { get; set; }
    public required GameStatus GameStatus { get; set; }
}