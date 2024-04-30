using FluentResults;
using GeoSpotter.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace GeoSpotter.API.Hubs;

public class NearbyPlacesHub : Hub
{
    public async override Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }

    public async Task SendLocationRequestParameter(RequestParameter requestParamter)
    {
        await Clients.All.SendAsync("ReceiveMessageRequest", requestParamter);
    }

    public async Task SendNearbyLocations(Result<FoursquareResponse> responseResult)
    {
        await Clients.All.SendAsync("ReceiveMessageResponse", responseResult);
    }
}
