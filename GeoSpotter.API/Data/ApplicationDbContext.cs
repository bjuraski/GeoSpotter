using GeoSpotter.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApiMessage> ApiMessages { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<FavouriteLocation> FavouriteLocations { get; set; }
}
