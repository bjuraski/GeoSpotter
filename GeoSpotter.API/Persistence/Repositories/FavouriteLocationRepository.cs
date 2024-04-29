using FluentResults;
using GeoSpotter.API.Data;
using GeoSpotter.API.DTOs;
using GeoSpotter.API.Mappings;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Persistence.Repositories;

public class FavouriteLocationRepository : IFavouriteLocationRepository
{
    private readonly ApplicationDbContextFactory _applicationDbContextFactory;

    public FavouriteLocationRepository(ApplicationDbContextFactory applicationDbContextFactory)
    {
        _applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task<Result<long>> AddFavouriteLocationAsync(FavouriteLocationDTO favouriteLocationDTO)
    {
        await using var dbContext = _applicationDbContextFactory.CreateDbContext();

        var favouriteLocation = FavouriteLocationMapping.MapDTOToEntity(favouriteLocationDTO);

        var entry = dbContext.Entry(favouriteLocation);
        entry.State = EntityState.Added;

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return Result.Ok(entry.Entity.Id);
        }

        return Result.Fail("Error occured during Favourite Location save");
    }

    public async Task<(bool IsExist, long? Id)> GetFavouriteLocationIdIfExistsAsync(FavouriteLocationDTO favouriteLocationDTO)
    {
        await using var dbContext = _applicationDbContextFactory.CreateDbContext();

        var existingFavouritePlace = await dbContext
            .FavouriteLocations
            .Where(l => l.UserId == favouriteLocationDTO.UserId
                && l.Latitude == favouriteLocationDTO.Latitude
                && l.Longitude == favouriteLocationDTO.Longitude)
            .SingleOrDefaultAsync();

        if (existingFavouritePlace is null)
        {
            return (false, null);
        }

        return (true, existingFavouritePlace.Id);
    }
}
