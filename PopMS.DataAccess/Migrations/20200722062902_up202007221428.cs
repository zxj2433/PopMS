using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007221428 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "inventories");

            migrationBuilder.AddColumn<string>(
                name: "PutUser",
                table: "inventories",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PutUser",
                table: "inventories");

            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "inventories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
