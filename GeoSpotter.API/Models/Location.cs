using System.Text.Json.Serialization;

namespace GeoSpotter.API.Models;

public class Location
{
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("formatted_address")]
    public string? FormattedAddress { get; set; }

    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    [JsonPropertyName("postcode")]
    public string? PostCode { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }
}
