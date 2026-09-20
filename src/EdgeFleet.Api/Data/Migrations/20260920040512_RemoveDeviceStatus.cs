using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdgeFleet.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeviceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
