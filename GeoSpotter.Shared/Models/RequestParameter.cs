namespace GeoSpotter.Shared.Models;

public record RequestParameter(
    long UserId,
    double Latitude,
    double Longitude,
    string? Categories,
    string? SearchTerm);
