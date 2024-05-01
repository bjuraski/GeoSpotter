using GeoSpotter.API.DTOs;
using GeoSpotter.API.Helpers;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoSpotter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritePlacesController : ControllerBase
{
    private readonly IFavouriteLocationRepository _favouriteLocationRepository;

    private static SemaphoreSlim _semaphore = new(1, 1);

    public FavouritePlacesController(IFavouriteLocationRepository favouriteLocationRepository)
    {
        _favouriteLocationRepository = favouriteLocationRepository;
    }

    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [HttpPost(Name = "SaveFavouriteLocation")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FavouriteLocationDTO))]
    public async Task<IActionResult> SaveFavouriteLocation(FavouriteLocationDTO favouriteLocationDTO)
    {
        await _semaphore.WaitAsync();

        try
        {
            var (isExist, id) = await _favouriteLocationRepository.GetFavouriteLocationIdIfExistsAsync(favouriteLocationDTO);

            if (isExist)
            {
                return CreatedAtAction(nameof(SaveFavouriteLocation), new { id }, favouriteLocationDTO);
            }

            var userIdResult = UserHelper.GetParsedUserId(HttpContext);

            if (userIdResult.IsFailed)
            {
                return BadRequest(string.Join(", ", userIdResult.Errors));
            }

            var result = await _favouriteLocationRepository.AddFavouriteLocationAsync(favouriteLocationDTO, userIdResult.Value);

            if (result.IsFailed)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }

            return CreatedAtAction(nameof(SaveFavouriteLocation), new { id = result.Value }, favouriteLocationDTO);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
