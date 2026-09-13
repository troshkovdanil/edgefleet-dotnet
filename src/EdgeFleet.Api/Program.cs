using EdgeFleet.Api.Domain;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var device = new Device
{
    Id = Guid.NewGuid(),
    Name = "Edge Device 01",
    Hostname = "edge-01"
};

app.MapGet("/", () => "EdgeFleet.NET");

app.MapGet("/devices", () => new[] { device });

app.Run();
