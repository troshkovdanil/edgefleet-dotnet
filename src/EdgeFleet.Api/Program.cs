using System.Text.Json.Serialization;
using EdgeFleet.Api.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

var app = builder.Build();

var device = new Device
{
    Id = Guid.NewGuid(),
    Name = "Edge Device 01",
    Hostname = "edge-01",
    Status = DeviceStatus.Online,
    LastSeenAt = DateTimeOffset.UtcNow
};

app.MapGet("/", () => "EdgeFleet.NET");

app.MapGet("/devices", () => new[] { device });

app.Run();
