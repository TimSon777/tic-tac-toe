using System.Linq.Expressions;
using Application.Abstractions.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations.Repositories;

public sealed class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountCurrentGamesAsync()
    {
        return await _context.Games
            .Where(IsCurrent)
            .CountAsync();
    }
    
    public async Task<IEnumerable<Game>> GetCurrentGamesAsync(int itemsCount, int pageNumber)
    {
        return await _context.Games
            .Where(IsCurrent)
            .OrderByDescending(g => g.CreatedDateTimeUtc)
            .ThenBy(g => g.Status)
            .Skip((pageNumber - 1) * itemsCount)
            .Take(itemsCount)
            .Include(g => g.Initiator.User)
            .ToListAsync();
    }

    private static readonly Expression<Func<Game, bool>> IsCurrent = game =>
        game.Status == GameStatus.NotStarted
        || game.Status == GameStatus.InProgress;
}