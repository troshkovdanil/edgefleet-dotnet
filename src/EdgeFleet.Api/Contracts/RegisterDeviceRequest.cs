namespace EdgeFleet.Api.Contracts;

public class RegisterDeviceRequest
{
    public string Name { get; init; } = string.Empty;

    public string Hostname { get; init; } = string.Empty;
}
