using Application.Abstractions;
using Application.Exceptions;
using Application.Models;
using Domain.Entities;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Implementations;

public sealed class UserManager : IUserManager
{
    private readonly UserManager<UserIdentity> _userManager;

    public UserManager(UserManager<UserIdentity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AccountingResult> CreateUserAsync(string userName, string password)
    {
        var userIdentity = new UserIdentity
        {
            UserName = userName
        };

        var result = await _userManager.CreateAsync(userIdentity, password);

        return result.MapToRegistrationResult();
    }

    public async Task<IUser?> FindByUserNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<IUser> GetByUserNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName)
               ?? throw new UserNotFoundException();
    }
    public async Task<bool> CheckCorrectAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
        {
            return false;
        }

        return await _userManager.CheckPasswordAsync(user, password);
    }
}