using GeoSpotter.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApiMessage> ApiMessages { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<FavouriteLocation> FavouriteLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasData(DatabaseSeed.GetUsers());

        modelBuilder.Entity<ApiMessage>()
            .HasOne(am => am.User)
            .WithMany()
            .HasForeignKey(am => am.UserId);
    }
}
