// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public sealed class RabbitMqSettings : ISettings
{
    public static string SectionName => "BROKER_SETTINGS";

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = default!;
    
    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = default!;
    
    [ConfigurationKeyName("USER_NAME")]
    public string UserName { get; set; } = default!;
}