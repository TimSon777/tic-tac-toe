using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<bool> IsActivePlayerAsync(string userName);
    Task<IEnumerable<User>> ListUsersWithHighestRatingAsync(int count);
}