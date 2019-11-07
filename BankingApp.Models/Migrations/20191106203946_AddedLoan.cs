using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.Models.Migrations
{
    public partial class AddedLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    InterestRate = table.Column<float>(nullable: false),
                    Term = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LoanId",
                table: "Transactions",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ApplicationUserId",
                table: "Loans",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Loans_LoanId",
                table: "Transactions",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Loans_LoanId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LoanId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Transactions");
        }
    }
}
