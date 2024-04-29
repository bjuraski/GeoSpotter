using GeoSpotter.API.DTOs;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoSpotter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritePlacesController : ControllerBase
{
    private readonly IFavouriteLocationRepository _favouriteLocationRepository;

    public FavouritePlacesController(IFavouriteLocationRepository favouriteLocationRepository)
    {
        _favouriteLocationRepository = favouriteLocationRepository;
    }

    [HttpPost(Name = "SaveFavouriteLocation")]
    public async Task<IActionResult> SaveFavouriteLocation(FavouriteLocationDTO favouriteLocationDTO)
    {
        var result = await _favouriteLocationRepository.AddFavouriteLocationAsync(favouriteLocationDTO);

        if (result.IsFailed)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }

        return CreatedAtAction(nameof(SaveFavouriteLocation), new { id = result.Value }, favouriteLocationDTO);
    }
}
