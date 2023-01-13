using Domain.Common;

namespace Domain.Entities;

public sealed class Player : BaseEntity<int>
{
    public User User { get; set; } = default!;
    public int UserId { get; set; }
}