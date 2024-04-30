using System.Text.Json.Serialization;

namespace GeoSpotter.Shared.Models;

public class GeoBounds
{
    [JsonPropertyName("circle")]
    public Circle? Circle { get; set; }
}
