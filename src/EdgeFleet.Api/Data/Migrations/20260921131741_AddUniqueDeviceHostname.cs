using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdgeFleet.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueDeviceHostname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Devices_Hostname",
                table: "Devices",
                column: "Hostname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_Hostname",
                table: "Devices");
        }
    }
}
