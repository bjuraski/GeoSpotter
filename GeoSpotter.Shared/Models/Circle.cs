using System.Text.Json.Serialization;

namespace GeoSpotter.Shared.Models;

public class Circle
{
    [JsonPropertyName("center")]
    public Center? Center { get; set; }

    [JsonPropertyName("radius")]
    public int Radius { get; set; }
}
