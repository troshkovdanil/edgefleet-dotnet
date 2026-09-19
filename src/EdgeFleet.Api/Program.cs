using System.Text.Json.Serialization;
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

var app = builder.Build();

app.MapGet("/", () => "EdgeFleet.NET");

app.MapGet("/devices", async (EdgeFleetDbContext dbContext) =>
{
    return await dbContext.Devices.ToListAsync();
});

app.MapGet("/devices/{id:guid}",
    async (Guid id, EdgeFleetDbContext dbContext) =>
{
    var device = await dbContext.Devices.FindAsync(id);

    return device is null
        ? Results.NotFound()
        : Results.Ok(device);
});

app.MapPost("/devices",
    async (RegisterDeviceRequest request, EdgeFleetDbContext dbContext) =>
{
    var device = new Device
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Hostname = request.Hostname,
        Status = DeviceStatus.Offline,
        LastSeenAt = null
    };

    dbContext.Devices.Add(device);

    await dbContext.SaveChangesAsync();

    return Results.Created($"/devices/{device.Id}", device);
});

app.Run();
