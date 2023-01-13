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

    public async Task UpdateGameAsync(Game game, Player matePlayer)
    {
        game.Mate = matePlayer;
        await _context.SaveChangesAsync();
    }

    public async Task<Game?> FindActiveGameByInitiatorUserNameAsync(string userName)
    {
        return await _context.Games.FirstOrDefaultAsync(g => g.Initiator.User.UserName == userName);
    }

    public async Task<Game> CreateGameAsync(string userName)
    {
        var user = await _context.Players.FirstAsync(u => u.User.UserName == userName);

        var game = new Game
        {
            Initiator = user
        };

        await _context.SaveChangesAsync();
        return game;
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