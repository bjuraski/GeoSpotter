using FluentResults;
using GeoSpotter.API.DTOs;

namespace GeoSpotter.API.Persistence.Interfaces;

public interface IFavouriteLocationRepository
{
    Task<Result<long>> AddFavouriteLocationAsync(FavouriteLocationDTO favouriteLocationDTO, long userId);

    Task<(bool IsExist, long? Id)> GetFavouriteLocationIdIfExistsAsync(FavouriteLocationDTO favouriteLocationDTO);
}
