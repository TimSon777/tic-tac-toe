using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public interface IUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }
}

public class User : BaseEntity<int>, IUser
{
    public string? UserName { get; set; }
    public ICollection<Player> Players { get; set; } = default!;
    
    public Game CurrentGame => Players.First(p => p.Game.Status == GameStatus.NotStarted).Game;

    public bool IsReadyToStartPlayer()
    {
        return Players.Any(p => p.Game.Status == GameStatus.NotStarted);
    }
}