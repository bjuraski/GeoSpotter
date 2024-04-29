using GeoSpotter.API.Clients;
using Microsoft.AspNetCore.Mvc;

namespace GeoSpotter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NearbyPlacesController : ControllerBase
{
    private readonly IFoursquareClient _foursquareClient;

    public NearbyPlacesController(IFoursquareClient foursquareClient)
    {
        _foursquareClient = foursquareClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetNearbyPlaces(double latitude, double longitude)
    {
        var nearbyPlacesResult = await _foursquareClient.GetNearbyPlacesByCoordinates(latitude, longitude);

        if (nearbyPlacesResult.IsFailed)
        {
            return BadRequest(nearbyPlacesResult.Errors);
        }

        return Ok(nearbyPlacesResult.Value);
    }
}
