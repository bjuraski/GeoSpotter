using GeoSpotter.API.DTOs;
using GeoSpotter.API.Entities;

namespace GeoSpotter.API.Mappings;

public static class FavouriteLocationMapping
{
    public static FavouriteLocation MapDTOToEntity(FavouriteLocationDTO favouriteLocation)
        => new()
        {
            UserId = favouriteLocation.UserId,
            Latitude = favouriteLocation.Latitude,
            Longitude = favouriteLocation.Longitude
        };
}
