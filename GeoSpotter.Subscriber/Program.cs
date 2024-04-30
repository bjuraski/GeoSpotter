using GeoSpotter.Shared;
using GeoSpotter.Shared.Consts;
using GeoSpotter.Shared.Models;
using GeoSpotter.Subscriber;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl(Endpoints.NearbyPlacesHubConnectionUrl)
    .WithAutomaticReconnect()
    .Build();

connection.On<RequestParameter>(HubConsts.NearbyPlacesHub.ReceiveMessageRequest, CreateMessage.RequestMessage);
connection.On<string>(HubConsts.NearbyPlacesHub.ReceiveMessageInvalidResponse, CreateMessage.InvalidResponse);
connection.On<FoursquareResponse?>(HubConsts.NearbyPlacesHub.ReceiveMessageValidResponse, CreateMessage.ValidResponse);

await connection.StartAsync();

Console.WriteLine($"Connected to SignalR Hub. Press Ctrl+C to exit.");
Console.ReadLine();

await connection.DisposeAsync();
