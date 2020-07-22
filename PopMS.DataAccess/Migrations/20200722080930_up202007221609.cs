using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007221609 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryouts_ship_Pop_Sums_spsumID",
                table: "inventoryouts");

            migrationBuilder.DropIndex(
                name: "IX_inventoryouts_spsumID",
                table: "inventoryouts");

            migrationBuilder.DropColumn(
                name: "spsumID",
                table: "inventoryouts");

            migrationBuilder.AddColumn<Guid>(
                name: "spID",
                table: "inventoryouts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "UsedQty",
                table: "inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventoryouts_spID",
                table: "inventoryouts",
                column: "spID");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryouts_ship_Pops_spID",
                table: "inventoryouts",
                column: "spID",
                principalTable: "ship_Pops",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryouts_ship_Pops_spID",
                table: "inventoryouts");

            migrationBuilder.DropIndex(
                name: "IX_inventoryouts_spID",
                table: "inventoryouts");

            migrationBuilder.DropColumn(
                name: "spID",
                table: "inventoryouts");

            migrationBuilder.DropColumn(
                name: "UsedQty",
                table: "inventories");

            migrationBuilder.AddColumn<Guid>(
                name: "spsumID",
                table: "inventoryouts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_inventoryouts_spsumID",
                table: "inventoryouts",
                column: "spsumID");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryouts_ship_Pop_Sums_spsumID",
                table: "inventoryouts",
                column: "spsumID",
                principalTable: "ship_Pop_Sums",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
