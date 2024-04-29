using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Data;

public class ApplicationDbContextFactory
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public ApplicationDbContextFactory(DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }

    public ApplicationDbContext CreateDbContext() => new(_dbContextOptions);
}
