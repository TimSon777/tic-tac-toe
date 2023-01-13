using Application.Abstractions;
using WebAPI.Implementations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class StartGameNotificatorConfiguration
{
    public static IServiceCollection AddStartGameNotificator(this IServiceCollection services)
    {
        services.AddSingleton<IStartGameNotificator, StartGameNotificator>();
        return services;
    }
}