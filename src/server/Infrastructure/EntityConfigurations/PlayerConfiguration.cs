using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Players");
        builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
        
        builder
            .Property(p => p.PlayerSign)
            .HasConversion<string>();
    }
}