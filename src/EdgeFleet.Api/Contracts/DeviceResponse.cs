using System.Linq.Expressions;
using EdgeFleet.Api.Domain;

namespace EdgeFleet.Api.Contracts;

public class DeviceResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Hostname { get; init; } = string.Empty;

    public DeviceStatus Status { get; init; }

    public DateTimeOffset? LastSeenAt { get; init; }

    public static Expression<Func<Device, DeviceResponse>> Projection =>
        device => new DeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            Hostname = device.Hostname,
            Status = device.Status,
            LastSeenAt = device.LastSeenAt
        };
}
