using System.Text.Json.Serialization;

namespace GeoSpotter.API.Models;

public class GeoBounds
{
    [JsonPropertyName("circle")]
    public Circle? Circle { get; set; }
}
