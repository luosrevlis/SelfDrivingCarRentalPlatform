using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Cars");
        }
    }
}
