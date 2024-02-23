using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Contracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractId = table.Column<int>(type: "integer", nullable: false),
                    InsuranceFee = table.Column<double>(type: "double precision", nullable: false),
                    Deposit = table.Column<double>(type: "double precision", nullable: false),
                    MortgageFee = table.Column<double>(type: "double precision", nullable: false),
                    DamageFee = table.Column<double>(type: "double precision", nullable: true),
                    LateReturnPenalty = table.Column<double>(type: "double precision", nullable: true),
                    OtherFees = table.Column<double>(type: "double precision", nullable: true),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TransactionId",
                table: "Contracts",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ContractId",
                table: "Transaction",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Transaction_TransactionId",
                table: "Contracts",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Transaction_TransactionId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_TransactionId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Contracts");

            migrationBuilder.AddColumn<double>(
                name: "Deposit",
                table: "Contracts",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Contracts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
