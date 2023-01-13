using Application.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations.Repositories;

public sealed class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _context;

    public PlayerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Player> CreatePlayerAsync(string userName)
    {
        var user = await _context.Users.FirstAsync(u => u.UserName == userName);

        var player = new Player
        {
            User = new User
            {
                Id = user.Id,
                UserName = userName
            }
        };

        _context.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
}