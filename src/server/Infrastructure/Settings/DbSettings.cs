// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public sealed class DbSettings : ISettings
{
    public static string SectionName => "DB_SETTINGS";

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = default!;

    [ConfigurationKeyName("PORT")]
    public int Port { get; set; }

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = default!;

    [ConfigurationKeyName("USER_NAME")]
    public string UserName { get; set; } = default!;

    [ConfigurationKeyName("NAME")]
    public string Name { get; set; } = default!;

    public string ToString(Database database)
    {
        return database switch
        {
            Database.Postgres => $"Host={Host}; User Id={UserName}; Password={Password}; Database={Name}; Port={Port}",
            _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
        };
    }
}

public enum Database
{
    Postgres
}