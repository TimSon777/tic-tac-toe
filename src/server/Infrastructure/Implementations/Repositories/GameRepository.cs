using System.Linq.Expressions;
using Application.Abstractions.Repositories;
using Application.Exceptions;
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

    public async Task<Game> GetActiveGameByUserNameAsync(string userName)
    {
        return await FindActiveGameByUserNameAsync(userName)
               ?? throw new EntityNotFoundException();
    }

    public async Task<Game> CreateGameAsync(Player initiator)
    {
        var game = new Game
        {
            Initiator = initiator
        };

        _context.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> FindActiveGameByUserNameAsync(string userName)
    {
        return await _context.Games
            .Include(g => g.Initiator)
            .ThenInclude(i => i.User)
            .Include(g => g.Mate)
            .ThenInclude(m => m!.User)
            .FirstOrDefaultAsync(g => (g.Status == GameStatus.InProgress || g.Status == GameStatus.NotStarted)
                                      && (g.Mate!.User.UserName == userName || g.Initiator.User.UserName == userName));
    }

    public async Task<Game> GetGameWithUsersByIdAsync(int gameId)
    {
        return await _context.Games
            .Include(g => g.Initiator.User)
            .Include(g => g.Mate!.User)
            .FirstAsync(g => g.Id == gameId);
    }

    public async Task UpdateAsync(Game game)
    {
        _context.Update(game);
        await _context.SaveChangesAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
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