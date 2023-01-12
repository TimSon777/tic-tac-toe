using Application.Abstractions;
using Application.Implementations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationMediatorConfiguration
{
    public static IServiceCollection AddApplicationMediator(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationMediator, ApplicationMediator>();
        return services;
    }
}