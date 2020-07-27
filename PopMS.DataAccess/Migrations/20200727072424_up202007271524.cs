using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007271524 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pops_PopNo",
                table: "pops");

            migrationBuilder.DropColumn(
                name: "PopNo",
                table: "pops");

            migrationBuilder.CreateIndex(
                name: "IX_pops_PopIndex",
                table: "pops",
                column: "PopIndex",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pops_PopIndex",
                table: "pops");

            migrationBuilder.AddColumn<string>(
                name: "PopNo",
                table: "pops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pops_PopNo",
                table: "pops",
                column: "PopNo",
                unique: true,
                filter: "[PopNo] IS NOT NULL");
        }
    }
}
