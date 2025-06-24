using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecast.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToLocationKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cities_LocationKey",
                table: "Cities",
                column: "LocationKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cities_LocationKey",
                table: "Cities");
        }
    }
}
