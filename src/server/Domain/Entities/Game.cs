using Domain.Common;
using Domain.Enums;
using Domain.Models;

namespace Domain.Entities;

public sealed class Game : BaseEntity<int>
{
    public Player Initiator { get; set; } = default!;
    public Player? Mate { get; set; }
    public Board Board { get; set; } = default!;
    public GameStatus Status { get; set; } = GameStatus.NotStarted;
    public DateTime CreatedDateTimeUtc { get; set; }
}