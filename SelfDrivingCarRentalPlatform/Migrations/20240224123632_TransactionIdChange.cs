using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class TransactionIdChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Transaction_TransactionId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Contracts_ContractId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ContractId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_TransactionId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Transaction",
                newName: "DuplicateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "DuplicateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Contracts_DuplicateId",
                table: "Transaction",
                column: "DuplicateId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Contracts_DuplicateId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "DuplicateId",
                table: "Transaction",
                newName: "ContractId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ContractId",
                table: "Transaction",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TransactionId",
                table: "Contracts",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Transaction_TransactionId",
                table: "Contracts",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Contracts_ContractId",
                table: "Transaction",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
