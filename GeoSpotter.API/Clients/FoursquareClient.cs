using FluentResults;
using GeoSpotter.API.Models;
using RestSharp;
using System.Globalization;
using System.Text.Json;

namespace GeoSpotter.API.Clients;

public class FoursquareClient : IFoursquareClient
{
    private readonly RestClient _client;
    private readonly IConfiguration _configuration;

    public FoursquareClient(IConfiguration configuration)
    {
        _configuration = configuration;

        var baseUrl = _configuration.GetValue<string>("Foursquare:ApiUrl");
        var options = new RestClientOptions(baseUrl ?? throw new ArgumentNullException("Foursquare API url can't be found"));

        _client = new RestClient(options);
    }

    public async Task<Result<FoursquareResponse>> GetNearbyPlacesByCoordinates(double latitude, double longitude, int radius = 1500)
    {
        var request = new RestRequest("");

        request.AddQueryParameter("ll", $"{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}");
        request.AddQueryParameter("radius", radius.ToString());

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", _configuration["FoursquareKeys:ApiKey"] ?? string.Empty);

        var response = await _client.GetAsync(request);

        if (response is null)
        {
            return Result.Fail($"Failed to fetch nearby places. Response is not unavailable");
        }

        if (!response.IsSuccessful)
        {
            return Result.Fail($"Failed to fetch nearby places. Status Code: {response.StatusCode}. Error: {response.ErrorMessage}");
        }

        if (string.IsNullOrEmpty(response.Content))
        {
            return Result.Fail($"Failed to fetch nearby places. Response Content is empty");
        }

        var nearbySearchResponse = JsonSerializer.Deserialize<FoursquareResponse>(response.Content);

        return Result.Ok(nearbySearchResponse!);
    }
}
