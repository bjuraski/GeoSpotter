using System.Text.Json.Serialization;

namespace GeoSpotter.Shared.Models;

public class Context
{
    [JsonPropertyName("geo_bounds")]
    public GeoBounds? GeoBounds { get; set; }
}
