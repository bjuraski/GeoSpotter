using FluentResults;
using GeoSpotter.API.Models;

namespace GeoSpotter.API.Clients;

public interface IFoursquareClient
{
    Task<Result<FoursquareResponse>> GetNearbyPlacesByCoordinates(
        double latitude,
        double longitude,
        string? categories = null,
        string? searchTerm = null);
}
