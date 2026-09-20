using EdgeFleet.Api.Domain;

namespace EdgeFleet.Api.Contracts;

public class DeviceResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Hostname { get; init; } = string.Empty;

    public DeviceStatus Status { get; init; }

    public DateTimeOffset? LastSeenAt { get; init; }
}
