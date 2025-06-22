using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecast.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyAndAdminAreaFieldsToCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdministrativeArea",
                table: "Cities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationKey",
                table: "Cities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministrativeArea",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "LocationKey",
                table: "Cities");
        }
    }
}
