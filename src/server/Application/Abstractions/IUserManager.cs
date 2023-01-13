using Application.Models;
using Domain.Entities;

namespace Application.Abstractions;

public interface IUserManager
{
    Task<AccountingResult> CreateUserAsync(IUser user, string password);
    Task<IUser?> FindByUserNameAsync(string userName);
    Task<bool> CheckCorrectAsync(string userName, string password);
    Task<IUser> GetByUserNameAsync(string userName);
}