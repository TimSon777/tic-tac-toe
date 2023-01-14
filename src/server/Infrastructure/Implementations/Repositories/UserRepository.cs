using Application.Abstractions.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsActivePlayerAsync(string userName)
    {
        return await _context.Games
            .AnyAsync(p => p.Initiator.User.UserName == userName || p.Mate != null && p.Mate.User.UserName == userName);
    }

    public async Task<User?> FindByUserNameWithReadyPlayerAsync(string userName)
    {
        var user = await _context.Users
            .Include(u => u.Players.Where(p => p.Game.Status == GameStatus.NotStarted))
            .ThenInclude(p => p.Game)
            .FirstOrDefaultAsync(u => u.UserName == userName);

        if (user is null)
        {
            return null;
        }

        return new User
        {
            Players = user.Players,
            UserName = userName
        };
    }
}