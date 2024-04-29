using FluentResults;
using GeoSpotter.API.Models;

namespace GeoSpotter.API.Clients;

public interface IFoursquareClient
{
    Task<Result<FoursquareResponse>> GetNearbyPlacesByCoordinates(double latitude, double longitude, int radius = 1500);
}
