using System.Text.Json.Serialization;
using EdgeFleet.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

builder.Services.AddSingleton<DeviceStore>();

var app = builder.Build();

app.MapGet("/", () => "EdgeFleet.NET");

app.MapGet("/devices", (DeviceStore deviceStore) =>
{
    return deviceStore.GetAll();
});

app.Run();
