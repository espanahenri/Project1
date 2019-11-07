using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.Models.Migrations
{
    public partial class loanispayed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPayed",
                table: "Loans",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPayed",
                table: "Loans");
        }
    }
}
