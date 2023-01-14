using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetCurrentGamesAsync(int itemsCount, int pageNumber);
    Task<int> CountCurrentGamesAsync();
    Task UpdateGameAsync(Game game, Player matePlayer);
    Task<Game?> FindActiveGameByInitiatorUserNameAsync(string userName);
    Task<Game> GetActiveGameByInitiatorUserNameAsync(string userName);
    Task<Game> CreateGameAsync(string userName);
    Task<Game?> FindGameByUserNameAsync(string userName);
    Task<Game> GetGameByIdAsync(int gameId);
    Task CommitAsync();
}