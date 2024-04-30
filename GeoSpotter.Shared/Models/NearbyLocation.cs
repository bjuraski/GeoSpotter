using System.Text.Json.Serialization;

namespace GeoSpotter.Shared.Models;

public class NearbyLocation
{
    [JsonPropertyName("fsq_id")]
    public required string FoursquareId { get; set; }

    [JsonPropertyName("categories")]
    public List<Category>? Categories { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("closed_bucket")]
    public string? ClosedBucket { get; set; }

    [JsonPropertyName("distance")]
    public int Distance { get; set; }

    [JsonPropertyName("timezone")]
    public string? TimeZone { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("geocodes")]
    public GeoCodes? GeoCodes { get; set; }
}
