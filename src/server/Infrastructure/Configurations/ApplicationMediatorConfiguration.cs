using Application;

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