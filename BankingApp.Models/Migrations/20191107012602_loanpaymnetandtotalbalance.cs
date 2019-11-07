using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.Models.Migrations
{
    public partial class loanpaymnetandtotalbalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentInstallment",
                table: "Loans",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBalance",
                table: "Loans",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentInstallment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "Loans");
        }
    }
}
