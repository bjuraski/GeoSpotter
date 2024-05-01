using FluentResults;
using GeoSpotter.Shared.Models;

namespace GeoSpotter.API.Clients;

public interface IFoursquareClient
{
    Task<Result<FoursquareResponse>> GetNearbyPlacesByCoordinates(
        long userId,
        double latitude,
        double longitude,
        string? categories = null,
        string? searchTerm = null);
}
