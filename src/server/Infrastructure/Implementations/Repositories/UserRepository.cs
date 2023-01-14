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
            .AnyAsync(g => (g.Status == GameStatus.InProgress || g.Status == GameStatus.NotStarted)
                           && (g.Initiator.User.UserName == userName || g.Mate!.User.UserName == userName));
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