using EdgeFleet.Api.Contracts;
using EdgeFleet.Api.Domain;
using Riok.Mapperly.Abstractions;

namespace EdgeFleet.Api.Mappers;

[Mapper]
public static partial class DeviceMapper
{
    [IncludeMappingConfiguration(nameof(MapDevice))]
    public static partial DeviceResponse ToResponse(Device device);

    public static partial IQueryable<DeviceResponse> ProjectToResponse(
        this IQueryable<Device> devices);

    [MapPropertyFromSource(
        nameof(DeviceResponse.DisplayName),
        Use = nameof(MapDisplayName))]
    private static partial DeviceResponse MapDevice(Device device);

    private static string MapDisplayName(Device device) =>
        $"{device.Name} ({device.Hostname})";
}
