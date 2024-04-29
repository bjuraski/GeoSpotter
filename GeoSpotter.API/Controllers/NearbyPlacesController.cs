using GeoSpotter.API.Clients;
using GeoSpotter.API.Models;
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

    [HttpGet(Name = "GetNearbyPlaces")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoursquareResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetNearbyPlaces(
        double latitude,
        double longitude,
        string? categories = null,
        string? searchTerm = null)
    {
        var nearbyPlacesResult = await _foursquareClient.GetNearbyPlacesByCoordinates(latitude, longitude, categories, searchTerm);

        if (nearbyPlacesResult.IsFailed)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, nearbyPlacesResult.Errors);
        }

        return Ok(nearbyPlacesResult.Value);
    }
}
