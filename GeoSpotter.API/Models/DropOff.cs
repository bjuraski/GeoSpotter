using System.Text.Json.Serialization;

namespace GeoSpotter.API.Models;

public class DropOff
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
