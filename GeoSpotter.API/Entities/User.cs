using System.ComponentModel.DataAnnotations;

namespace GeoSpotter.API.Entities;

public class User
{
    public long Id { get; set; }

    [MaxLength(50)]
    public required string UserName { get; set; }

    [MaxLength(30)]
    public required string Password { get; set; }
}
