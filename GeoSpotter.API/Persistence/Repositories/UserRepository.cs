using FluentResults;
using GeoSpotter.API.Data;
using GeoSpotter.API.Entities;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContextFactory _applicationDbContextFactory;

    public UserRepository(ApplicationDbContextFactory applicationDbContextFactory)
    {
        _applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task<Result<User>> TryGetUserByUsernameAndPassword(string userName, string password)
    {
        await using var dbContext = _applicationDbContextFactory.CreateDbContext();

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
