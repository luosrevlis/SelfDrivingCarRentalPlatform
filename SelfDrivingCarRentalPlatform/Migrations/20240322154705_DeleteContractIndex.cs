using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class DeleteContractIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contracts_CarId_RentStartDate",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CarId",
                table: "Contracts",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contracts_CarId",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CarId_RentStartDate",
                table: "Contracts",
                columns: new[] { "CarId", "RentStartDate" },
                unique: true);
        }
    }
}
