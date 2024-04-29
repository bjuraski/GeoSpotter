using System.Text.Json.Serialization;

namespace GeoSpotter.API.Models;

public class Main
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
