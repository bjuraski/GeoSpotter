using System.Text.Json.Serialization;

namespace GeoSpotter.API.Models;

public class GeoCodes
{
    [JsonPropertyName("drop_off")]
    public DropOff? DropOff { get; set; }

    [JsonPropertyName("main")]
    public Main? Main { get; set; }

    [JsonPropertyName("roof")]
    public Roof? Roof { get; set; }
}
