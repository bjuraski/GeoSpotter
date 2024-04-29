namespace GeoSpotter.API.Entities;

public class FavouriteLocation
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
