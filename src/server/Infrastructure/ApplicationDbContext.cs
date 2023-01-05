using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
}