namespace EdgeFleet.Api.Domain;

public class Device
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Hostname { get; init; } = string.Empty;
}
