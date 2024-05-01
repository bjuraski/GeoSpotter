using GeoSpotter.Shared.Models;

namespace GeoSpotter.Subscriber;

public static class CreateMessage
{
    public static void RequestMessage(RequestParameter requestParameter)
    {
        var categoryIds = !string.IsNullOrEmpty(requestParameter.Categories)
            ? requestParameter.Categories.Split(',')
            : null;

        Console.WriteLine($"""

            Received new Location Request:
                User Id: {requestParameter.UserId}
                Latitude: {requestParameter.Latitude}
                Longitude: {requestParameter.Longitude}
                Category Ids: {(categoryIds != null ? string.Join(", ", categoryIds) : string.Empty)}
                Search Term: {requestParameter.SearchTerm ?? string.Empty}

            """);
    }

    public static void InvalidResponse(string error)
    {
        Console.WriteLine($"""

            Request Failed with error:
                {error}

            """);
    }

    public static void ValidResponse(FoursquareResponse? response)
    {
        var nearbyLocations = response?
            .NearbyLocations?
            .Select(l => new
            {
                Name = l.Name,
                Address = l.Location?.Address ?? "N/A",
                Categories = l.Categories?.Select(c => c.Name).ToArray()
            })
            .ToList();

        if (nearbyLocations is null || !nearbyLocations.Any())
        {
            Console.WriteLine("""

                For given request no locations nearby found!

                """);
        }
        else
        {
            Console.WriteLine("""

                Nearby Locations for request:
                """);
        }


        foreach (var nearbyLocation in nearbyLocations!)
        {
            var categoryNames = nearbyLocation.Categories is not null && nearbyLocation.Categories.Any()
                ? string.Join(", ", nearbyLocation.Categories)
                : string.Empty;

            Console.WriteLine($"""
                Name: {nearbyLocation.Name}
                Address: {nearbyLocation.Address},
                Categories: {categoryNames}

            """);
        }
    }
}
