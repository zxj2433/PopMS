using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007281907 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "order_Pops",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "order_Pops");
        }
    }
}
