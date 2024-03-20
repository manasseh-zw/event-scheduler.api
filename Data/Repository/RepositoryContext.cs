using event_scheduler.api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace event_scheduler.api.Data.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.Events)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<EventModel> Events { get; set; }
}