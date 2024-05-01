namespace GeoSpotter.API.Entities;

public class ApiMessage
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public User? User { get; set; }

    public required string RequestJson { get; set; }

    public required string ResponseJson { get; set; }
}
