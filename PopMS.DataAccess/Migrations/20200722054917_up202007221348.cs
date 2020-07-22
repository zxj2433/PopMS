using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007221348 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_depts_dcs_DCID",
                table: "depts");

            migrationBuilder.DropIndex(
                name: "IX_depts_DCID",
                table: "depts");

            migrationBuilder.DropColumn(
                name: "DCID",
                table: "depts");

            migrationBuilder.AddColumn<Guid>(
                name: "DCID",
                table: "FrameworkUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUsers_DCID",
                table: "FrameworkUsers",
                column: "DCID");

            migrationBuilder.AddForeignKey(
                name: "FK_FrameworkUsers_dcs_DCID",
                table: "FrameworkUsers",
                column: "DCID",
                principalTable: "dcs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FrameworkUsers_dcs_DCID",
                table: "FrameworkUsers");

            migrationBuilder.DropIndex(
                name: "IX_FrameworkUsers_DCID",
                table: "FrameworkUsers");

            migrationBuilder.DropColumn(
                name: "DCID",
                table: "FrameworkUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "DCID",
                table: "depts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_depts_DCID",
                table: "depts",
                column: "DCID");

            migrationBuilder.AddForeignKey(
                name: "FK_depts_dcs_DCID",
                table: "depts",
                column: "DCID",
                principalTable: "dcs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
