using EdgeFleet.Api.Domain;

namespace EdgeFleet.Api.Services;

public class DeviceStore
{
    private readonly List<Device> devices =
    [
        new Device
        {
            Id = Guid.NewGuid(),
            Name = "Edge Device 01",
            Hostname = "edge-01",
            Status = DeviceStatus.Online,
            LastSeenAt = DateTimeOffset.UtcNow
        }
    ];

    public IReadOnlyList<Device> GetAll()
    {
        return devices;
    }

    public Device? GetById(Guid id)
    {
        return devices.FirstOrDefault(device => device.Id == id);
    }

    public void Add(Device device)
    {
        devices.Add(device);
    }
}
