using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions;
using Application.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Implementations;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(IOptions<JwtSettings> jwtSettingsOptions)
    {
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public JwtResult Generate(IUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName!),
        };
        
        var claimsIdentity = new ClaimsIdentity(claims);

        var jwt = new JwtSecurityToken(
            issuer: null,
            audience: null,
            notBefore: null,
            claims: claimsIdentity.Claims,
            expires: null,
            signingCredentials: new SigningCredentials(_jwtSettings.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
        
        return new JwtResult
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt)
        };
    }
}