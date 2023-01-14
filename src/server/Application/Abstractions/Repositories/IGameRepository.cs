using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetCurrentGamesAsync(int itemsCount, int pageNumber);
    Task<int> CountCurrentGamesAsync();
    Task UpdateGameAsync(Game game, Player matePlayer);
    Task<Game?> FindActiveGameByUserNameAsync(string userName);
    Task<Game> GetActiveGameByUserNameAsync(string userName);
    Task<Game> CreateGameAsync(Player initiator);
    Task<Game> GetGameWithUsersByIdAsync(int gameId);
    Task CommitAsync();
}