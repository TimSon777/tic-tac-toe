using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.HasOne<User>().WithOne().HasForeignKey<User>(u => u.Id);
        builder.Property(u => u.UserName).HasColumnName("UserName");
    }
}