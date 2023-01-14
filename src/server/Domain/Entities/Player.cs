using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Player : BaseEntity<int>
{
    public User User { get; set; } = default!;
    public int UserId { get; set; }
    public PlayerSign PlayerSign { get; set; }
    public Game Game { get; set; } = default!;
}