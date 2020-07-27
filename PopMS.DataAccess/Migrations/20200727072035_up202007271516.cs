using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007271516 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pops_PopNo",
                table: "pops");

            migrationBuilder.AlterColumn<string>(
                name: "PopNo",
                table: "pops",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_pops_PopNo",
                table: "pops",
                column: "PopNo",
                unique: true,
                filter: "[PopNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pops_PopNo",
                table: "pops");

            migrationBuilder.AlterColumn<string>(
                name: "PopNo",
                table: "pops",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pops_PopNo",
                table: "pops",
                column: "PopNo",
                unique: true);
        }
    }
}
