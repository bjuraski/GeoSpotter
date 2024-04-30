using FluentResults;
using GeoSpotter.API.Entities;
using GeoSpotter.API.Persistence.Interfaces;
using GeoSpotter.Shared.Models;
using RestSharp;
using System.Globalization;
using System.Text.Json;

namespace GeoSpotter.API.Clients;

public class FoursquareClient : IFoursquareClient
{
    private readonly RestClient _client;
    private readonly IConfiguration _configuration;
    private readonly IApiMessageRepository _apiMessageRepository;

    public FoursquareClient(IConfiguration configuration, IApiMessageRepository apiMessageRepository)
    {
        _configuration = configuration;
        _apiMessageRepository = apiMessageRepository;

        var baseUrl = _configuration.GetValue<string>("Foursquare:ApiUrl");
        var options = new RestClientOptions(baseUrl ?? throw new ArgumentNullException("Foursquare API url can't be found"));

        _client = new RestClient(options);
    }

    public async Task<Result<FoursquareResponse>> GetNearbyPlacesByCoordinates(
        double latitude,
        double longitude,
        string? categories = null,
        string? searchTerm = null)
    {
        var request = CreateRequest(latitude, longitude, categories, searchTerm);
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

        await SaveApiMessageAsync(request, response.Content);

        var nearbySearchResponse = JsonSerializer.Deserialize<FoursquareResponse>(response.Content);

        return Result.Ok(nearbySearchResponse!);
    }

    private RestRequest CreateRequest(
        double latitude,
        double longitude,
        string? categories = null,
        string? searchTerm = null)
    {
        var request = new RestRequest("");

        request.AddQueryParameter("ll", $"{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}");
        request.AddQueryParameter("radius", (_configuration.GetValue<int>("Foursquare:SearchReadius")).ToString());

        if (!string.IsNullOrEmpty(categories))
        {
            request.AddQueryParameter("categories", categories);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            request.AddQueryParameter("query", searchTerm);
        }

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", _configuration["FoursquareKeys:ApiKey"] ?? string.Empty);

        return request;
    }

    private async Task SaveApiMessageAsync(RestRequest request, string response)
    {
        var apiMessage = new ApiMessage
        {
            RequestJson = JsonSerializer.Serialize(request),
            ResponseJson = response
        };

        await _apiMessageRepository.AddApiMessageAsync(apiMessage);
    }
}
