using GeoSpotter.API.Data;
using GeoSpotter.API.Entities;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Persistence.Repositories;

public class ApiMessageRepository : IApiMessageRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public ApiMessageRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task AddApiMessageAsync(ApiMessage apiMessage)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();

        await dbContext.ApiMessages.AddAsync(apiMessage);

        await dbContext.SaveChangesAsync();
    }
}
