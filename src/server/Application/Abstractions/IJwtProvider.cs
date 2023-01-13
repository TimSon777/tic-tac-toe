using Application.Models;
using Domain.Entities;

namespace Application.Abstractions;

public interface IJwtProvider
{
    JwtResult Generate(IUser user);
}