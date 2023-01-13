using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSettings<TSettings>(this IServiceCollection services,
        IConfiguration configuration)
        where TSettings : class, ISettings
    {
        services.Configure<TSettings>(configuration.GetSection(TSettings.SectionName));
        return services;
    }
}