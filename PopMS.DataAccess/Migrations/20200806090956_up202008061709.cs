using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202008061709 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pop_Groups_dcs_DCID",
                table: "pop_Groups");

            migrationBuilder.DropIndex(
                name: "IX_pop_Groups_DCID",
                table: "pop_Groups");

            migrationBuilder.DropColumn(
                name: "DCID",
                table: "pop_Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DCID",
                table: "pop_Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_pop_Groups_DCID",
                table: "pop_Groups",
                column: "DCID");

            migrationBuilder.AddForeignKey(
                name: "FK_pop_Groups_dcs_DCID",
                table: "pop_Groups",
                column: "DCID",
                principalTable: "dcs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
