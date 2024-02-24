using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class RenameTransactionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Contracts_DuplicateId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "DuplicateId",
                table: "Transaction",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Contracts_Id",
                table: "Transaction",
                column: "Id",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Contracts_Id",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transaction",
                newName: "DuplicateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Contracts_DuplicateId",
                table: "Transaction",
                column: "DuplicateId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
