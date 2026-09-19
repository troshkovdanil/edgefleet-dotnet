using System.Text.Json.Serialization;
using EdgeFleet.Api.Services;
using EdgeFleet.Api.Domain;
using EdgeFleet.Api.Contracts;
using EdgeFleet.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

builder.Services.AddValidation();

var connectionString =
    builder.Configuration.GetConnectionString("EdgeFleet")
    ?? throw new InvalidOperationException(
        "Connection string 'EdgeFleet' was not found.");

builder.Services.AddDbContext<EdgeFleetDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<DeviceStore>();

var app = builder.Build();

app.MapGet("/", () => "EdgeFleet.NET");

app.MapGet("/devices", (DeviceStore deviceStore) =>
{
    return deviceStore.GetAll();
});

app.MapGet("/devices/{id:guid}", (Guid id, DeviceStore deviceStore) =>
{
    var device = deviceStore.GetById(id);

    return device is null
        ? Results.NotFound()
        : Results.Ok(device);
});

app.MapPost("/devices",
    (RegisterDeviceRequest request, DeviceStore deviceStore) =>
{
    var device = new Device
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Hostname = request.Hostname,
        Status = DeviceStatus.Offline,
        LastSeenAt = DateTimeOffset.UtcNow
    };

    deviceStore.Add(device);

    return Results.Created($"/devices/{device.Id}", device);
});

app.Run();
