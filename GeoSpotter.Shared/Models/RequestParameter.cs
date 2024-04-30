namespace GeoSpotter.Shared.Models;

public record RequestParameter(
    double Latitude,
    double Longitude,
    string? Categories,
    string? SearchTerm);
