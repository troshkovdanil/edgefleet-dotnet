using EdgeFleet.Api.Contracts;
using EdgeFleet.Api.Domain;
using Riok.Mapperly.Abstractions;

namespace EdgeFleet.Api.Mappers;

[Mapper]
public static partial class DeviceMapper
{
    public static partial DeviceResponse ToResponse(Device device);

    public static partial IQueryable<DeviceResponse> ProjectToResponse(
        this IQueryable<Device> devices);
}
