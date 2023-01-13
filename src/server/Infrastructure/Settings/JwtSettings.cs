using System.Text;
using Microsoft.IdentityModel.Tokens;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public sealed class JwtSettings : ISettings
{
    public static string SectionName => "JWT_SETTINGS";

    [ConfigurationKeyName("SECRET_KEY")]
    public string SecretKey { get; set; } = default!;

    public SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.ASCII.GetBytes(SecretKey));
}