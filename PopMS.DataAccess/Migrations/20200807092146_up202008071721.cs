using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202008071721 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageID",
                table: "pops",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pack",
                table: "pops",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "pops",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "pops",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_pops_ImageID",
                table: "pops",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_pops_FileAttachments_ImageID",
                table: "pops",
                column: "ImageID",
                principalTable: "FileAttachments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pops_FileAttachments_ImageID",
                table: "pops");

            migrationBuilder.DropIndex(
                name: "IX_pops_ImageID",
                table: "pops");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "pops");

            migrationBuilder.DropColumn(
                name: "Pack",
                table: "pops");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "pops");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "pops");
        }
    }
}
