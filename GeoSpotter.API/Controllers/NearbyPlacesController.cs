using GeoSpotter.API.Clients;
using GeoSpotter.API.Hubs;
using GeoSpotter.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GeoSpotter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NearbyPlacesController : ControllerBase
{
    private readonly IHubContext<NearbyPlacesHub> _hubContext;
    private readonly IFoursquareClient _foursquareClient;

    public NearbyPlacesController(IHubContext<NearbyPlacesHub> hubContext, IFoursquareClient foursquareClient)
    {
        _hubContext = hubContext;
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
        var requestParameter = new RequestParameter(latitude, longitude, categories, searchTerm);

        await _hubContext.Clients.All.SendAsync("ReceiveMessageRequest", requestParameter);

        var nearbyPlacesResult = await _foursquareClient.GetNearbyPlacesByCoordinates(latitude, longitude, categories, searchTerm);

        if (nearbyPlacesResult.IsFailed)
        {
            var error = string.Join(", ", nearbyPlacesResult.Errors);

            await _hubContext.Clients.All.SendAsync("ReceiveMessageInvalidResponse", error);

            return StatusCode(StatusCodes.Status500InternalServerError, nearbyPlacesResult.Errors);
        }

        await _hubContext.Clients.All.SendAsync("ReceiveMessageValidResponse", nearbyPlacesResult.Value);

        return Ok(nearbyPlacesResult.Value);
    }
}
