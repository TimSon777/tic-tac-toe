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
            .AnyAsync(p => p.Initiator.User.UserName == userName || p.Mate!.User.UserName == userName);
    }

    public async Task<User?> FindByUserNameWithReadyPlayerAsync(string userName)
    {
        var game = await _context.Games
            .Include(g => g.Initiator)
            .Include(g => g.Mate)
            .FirstOrDefaultAsync(g => g.Status == GameStatus.NotStarted && (g.Mate!.User.UserName == userName || g.Initiator.User.UserName == userName));

        if (game is null)
        {
            return null;
        }

        return new User
        {
            UserName = userName
        };
    }

    public async Task<IEnumerable<User>> ListUsersWithHighestRatingAsync(int count)
    {
        var users = await _context.Users
            .OrderByDescending(u => u.Rating)
            .Take(count)
            .ToListAsync();

        return users.Select(u => new User
        {
            Id = u.Id,
            Rating = u.Rating,
            UserName = u.UserName
        });
    }
}