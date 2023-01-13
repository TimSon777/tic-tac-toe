using Application.Abstractions;
using Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class JwtConfiguration
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSettings<JwtSettings>(configuration);
        services.AddSingleton<IJwtProvider, JwtProvider>();
        return services;
    }
}