// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

// ReSharper disable once InconsistentNaming
public static class IConfigurationExtensions
{
    public static TSettings GetSettings<TSettings>(this IConfiguration configuration,
        string? sectionName = default)
        where TSettings : ISettings
    {
        sectionName ??= TSettings.SectionName;

        return configuration
            .GetRequiredSection(sectionName)
            .Get<TSettings>()
            ?? throw new InvalidOperationException($"No settings under {sectionName} section name");
    }
}