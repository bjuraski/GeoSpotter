namespace GeoSpotter.API.Entities;

public record ApiMessage
{
    public long Id { get; set; }

    public required string RequestJson { get; set; }

    public required string ResponseJson { get; set; }
}
