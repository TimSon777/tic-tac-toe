using Application.Abstractions;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Identity;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddDefaultIdentity<UserIdentity>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequiredLength = 3,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IUserManager, UserManager>();
        return services;
    }
}