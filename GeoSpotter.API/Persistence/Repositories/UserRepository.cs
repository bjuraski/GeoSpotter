using FluentResults;
using GeoSpotter.API.Data;
using GeoSpotter.API.Entities;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Result<User>> TryGetUserByUsernameAndPassword(string userName, string password)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();

        var user = await dbContext
            .Users
            .Where(u => u.UserName == userName && u.Password == password)
            .SingleOrDefaultAsync();

        if (user is null)
        {
            return Result.Fail($"User with username {userName} does not exist");
        }

        return Result.Ok(user);
    }
}
