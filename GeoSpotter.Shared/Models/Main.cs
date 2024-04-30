using System.Text.Json.Serialization;

namespace GeoSpotter.Shared.Models;

public class Main
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
