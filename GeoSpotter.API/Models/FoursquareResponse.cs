using System.Text.Json.Serialization;

namespace GeoSpotter.API.Models;

public class FoursquareResponse
{
    [JsonPropertyName("results")]
    public List<NearbyLocation>? NearbyLocations { get; set; }

    [JsonPropertyName("context")]
    public Context? Context { get; set; }
}
