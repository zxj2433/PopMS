using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007221446 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryIn_contract_Pops_contract_popID",
                table: "inventoryIn");

            migrationBuilder.DropIndex(
                name: "IX_inventoryIn_contract_popID",
                table: "inventoryIn");

            migrationBuilder.DropColumn(
                name: "contract_popID",
                table: "inventoryIn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "contract_popID",
                table: "inventoryIn",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventoryIn_contract_popID",
                table: "inventoryIn",
                column: "contract_popID");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryIn_contract_Pops_contract_popID",
                table: "inventoryIn",
                column: "contract_popID",
                principalTable: "contract_Pops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
