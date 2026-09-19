using EdgeFleet.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace EdgeFleet.Api.Data;

public class EdgeFleetDbContext : DbContext
{
    public EdgeFleetDbContext(
        DbContextOptions<EdgeFleetDbContext> options)
        : base(options)
    {
    }

    public DbSet<Device> Devices => Set<Device>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.Property(device => device.Name)
                .HasMaxLength(40);

            entity.Property(device => device.Hostname)
                .HasMaxLength(40);
        });
    }
}
