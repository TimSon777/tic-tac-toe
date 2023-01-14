using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public sealed class UserIdentity : IdentityUser<int>, IUser
{
    public ICollection<Player> Players { get; set; } = default!;
}