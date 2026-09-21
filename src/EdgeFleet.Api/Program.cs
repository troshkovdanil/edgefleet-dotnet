using System.Text.Json.Serialization;
using EdgeFleet.Api.Domain;
using EdgeFleet.Api.Contracts;
using EdgeFleet.Api.Data;
using Microsoft.EntityFrameworkCore;
using EdgeFleet.Api.Mappers;
using Npgsql;

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
    return await dbContext.Devices
        .AsNoTracking()
        .ProjectToResponse()
        .ToListAsync();
});

app.MapGet("/devices/{id:guid}",
    async (Guid id, EdgeFleetDbContext dbContext) =>
{
    var device = await dbContext.Devices
        .AsNoTracking()
        .Where(device => device.Id == id)
        .ProjectToResponse()
        .FirstOrDefaultAsync();

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
        LastSeenAt = null
    };

    dbContext.Devices.Add(device);

    try
    {
        await dbContext.SaveChangesAsync();
    }
    catch (DbUpdateException exception)
        when (exception.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.UniqueViolation,
            ConstraintName: "IX_Devices_Hostname"
        })
    {
        return Results.Conflict(new
        {
            error = $"A device with hostname '{request.Hostname}' already exists."
        });
    }

    var response = DeviceMapper.ToResponse(device);

    return Results.Created($"/devices/{device.Id}", response);
});

app.MapPost("/devices/{id:guid}/heartbeat",
    async (Guid id, EdgeFleetDbContext dbContext) =>
{
    var device = await dbContext.Devices.FindAsync(id);

    if (device is null)
    {
        return Results.NotFound();
    }

    device.LastSeenAt = DateTimeOffset.UtcNow;

    await dbContext.SaveChangesAsync();

    return Results.Ok(DeviceMapper.ToResponse(device));
});

app.Run();
