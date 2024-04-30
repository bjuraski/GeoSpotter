using GeoSpotter.Shared.Models;
using GeoSpotter.Subscriber;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7060/nearby-places-hub")
    .WithAutomaticReconnect()
    .Build();

connection.On<RequestParameter>("ReceiveMessageRequest", CreateMessage.RequestMessage);
connection.On<string>("ReceiveMessageInvalidResponse", CreateMessage.InvalidResponse);
connection.On<FoursquareResponse?>("ReceiveMessageValidResponse", CreateMessage.ValidResponse);

await connection.StartAsync();

Console.WriteLine($"Connected to SignalR Hub. Press Ctrl+C to exit.");
Console.ReadLine();

await connection.DisposeAsync();
