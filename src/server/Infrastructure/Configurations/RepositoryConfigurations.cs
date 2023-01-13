using Application.Abstractions.Repositories;
using Infrastructure.Implementations.Repositories;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RepositoryConfigurations
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();
        return services;
    }
}