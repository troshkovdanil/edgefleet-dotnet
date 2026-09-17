using System.ComponentModel.DataAnnotations;

namespace EdgeFleet.Api.Contracts;

public class RegisterDeviceRequest
{
    [Required(ErrorMessage = "Device name is required")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Device name must be between 3 and 40 characters")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Hostname is required")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Hostname must be between 3 and 40 characters")]
    public string Hostname { get; init; } = string.Empty;
}
