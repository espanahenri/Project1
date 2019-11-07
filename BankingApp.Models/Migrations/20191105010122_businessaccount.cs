using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.Models.Migrations
{
    public partial class businessaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessAccountId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "InterestRate",
                table: "CheckingAccounts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "BusinessAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Overdraft = table.Column<decimal>(nullable: false),
                    InterestRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAccounts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BusinessAccountId",
                table: "Transactions",
                column: "BusinessAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAccounts_ApplicationUserId",
                table: "BusinessAccounts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BusinessAccounts_BusinessAccountId",
                table: "Transactions",
                column: "BusinessAccountId",
                principalTable: "BusinessAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BusinessAccounts_BusinessAccountId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BusinessAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BusinessAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BusinessAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "CheckingAccounts");
        }
    }
}
