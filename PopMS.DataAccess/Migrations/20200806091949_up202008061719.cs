using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202008061719 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OutID",
                table: "pops",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pops_OutID",
                table: "pops",
                column: "OutID",
                unique: true,
                filter: "[OutID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pops_OutID",
                table: "pops");

            migrationBuilder.DropColumn(
                name: "OutID",
                table: "pops");
        }
    }
}
