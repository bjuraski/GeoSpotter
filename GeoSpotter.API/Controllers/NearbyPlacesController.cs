using GeoSpotter.API.Clients;
using GeoSpotter.API.Helpers;
using GeoSpotter.API.Hubs;
using GeoSpotter.Shared.Consts;
using GeoSpotter.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GeoSpotter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NearbyPlacesController : ControllerBase
{
    private readonly IHubContext<NearbyPlacesHub> _hubContext;
    private readonly IFoursquareClient _foursquareClient;

    private static SemaphoreSlim _semaphore = new(1, 1);

    public NearbyPlacesController(IHubContext<NearbyPlacesHub> hubContext, IFoursquareClient foursquareClient)
    {
        _hubContext = hubContext;
        _foursquareClient = foursquareClient;
    }

    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [HttpGet(Name = "GetNearbyPlaces")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoursquareResponse))]
    public async Task<IActionResult> GetNearbyPlaces(
        double latitude,
        double longitude,
        string? categories = null,
        string? searchTerm = null)
    {
        await _semaphore.WaitAsync();

        try
        {
            var userIdResult = UserHelper.GetParsedUserId(HttpContext);

            if (userIdResult.IsFailed)
            {
                return BadRequest(string.Join(", ", userIdResult.Errors));
            }

            var requestParameter = new RequestParameter(userIdResult.Value, latitude, longitude, categories, searchTerm);

            await _hubContext.Clients.All.SendAsync(HubConsts.NearbyPlacesHub.ReceiveMessageRequest, requestParameter);

            var nearbyPlacesResult = await _foursquareClient.GetNearbyPlacesByCoordinates(userIdResult.Value, latitude, longitude, categories, searchTerm);

            if (nearbyPlacesResult.IsFailed)
            {
                var error = string.Join(", ", nearbyPlacesResult.Errors);

                await _hubContext.Clients.All.SendAsync(HubConsts.NearbyPlacesHub.ReceiveMessageInvalidResponse, error);

                return StatusCode(StatusCodes.Status500InternalServerError, nearbyPlacesResult.Errors);
            }

            await _hubContext.Clients.All.SendAsync(HubConsts.NearbyPlacesHub.ReceiveMessageValidResponse, nearbyPlacesResult.Value);

            return Ok(nearbyPlacesResult.Value);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
