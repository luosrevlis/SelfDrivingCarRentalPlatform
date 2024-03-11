using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ContractStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractStatus",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractStatus",
                table: "Contracts");
        }
    }
}
