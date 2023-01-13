using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetCurrentGamesAsync(int itemsCount, int pageNumber);
    Task<int> CountCurrentGamesAsync();
}