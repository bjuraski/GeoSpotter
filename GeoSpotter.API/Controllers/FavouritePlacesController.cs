using GeoSpotter.API.DTOs;
using GeoSpotter.API.Persistence.Interfaces;
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

    [HttpPost(Name = "SaveFavouriteLocation")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FavouriteLocationDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            var result = await _favouriteLocationRepository.AddFavouriteLocationAsync(favouriteLocationDTO);

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
