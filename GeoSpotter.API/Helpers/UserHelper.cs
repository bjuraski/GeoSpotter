using FluentResults;
using System.Security.Claims;

namespace GeoSpotter.API.Helpers;

public static class UserHelper
{
    public static Result<long> GetParsedUserId(HttpContext httpContext)
    {
        var userIdString = httpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        var isUserParsed = long.TryParse(userIdString, out var userId);

        if (isUserParsed)
        {
            return Result.Ok(userId);
        }

        return Result.Fail("Invalid User Id");
    }
}
