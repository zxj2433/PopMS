using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007281426 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_Pops_contract_Pops_ContractPopContractID_ContractPopPopID",
                table: "order_Pops");

            migrationBuilder.DropIndex(
                name: "IX_order_Pops_ContractPopContractID_ContractPopPopID",
                table: "order_Pops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contract_Pops",
                table: "contract_Pops");

            migrationBuilder.DropColumn(
                name: "ContractPopContractID",
                table: "order_Pops");

            migrationBuilder.DropColumn(
                name: "ContractPopPopID",
                table: "order_Pops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contract_Pops",
                table: "contract_Pops",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_order_Pops_ContractPopID",
                table: "order_Pops",
                column: "ContractPopID");

            migrationBuilder.CreateIndex(
                name: "IX_contract_Pops_ContractID",
                table: "contract_Pops",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_order_Pops_contract_Pops_ContractPopID",
                table: "order_Pops",
                column: "ContractPopID",
                principalTable: "contract_Pops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_Pops_contract_Pops_ContractPopID",
                table: "order_Pops");

            migrationBuilder.DropIndex(
                name: "IX_order_Pops_ContractPopID",
                table: "order_Pops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contract_Pops",
                table: "contract_Pops");

            migrationBuilder.DropIndex(
                name: "IX_contract_Pops_ContractID",
                table: "contract_Pops");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractPopContractID",
                table: "order_Pops",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractPopPopID",
                table: "order_Pops",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_contract_Pops",
                table: "contract_Pops",
                columns: new[] { "ContractID", "PopID" });

            migrationBuilder.CreateIndex(
                name: "IX_order_Pops_ContractPopContractID_ContractPopPopID",
                table: "order_Pops",
                columns: new[] { "ContractPopContractID", "ContractPopPopID" });

            migrationBuilder.AddForeignKey(
                name: "FK_order_Pops_contract_Pops_ContractPopContractID_ContractPopPopID",
                table: "order_Pops",
                columns: new[] { "ContractPopContractID", "ContractPopPopID" },
                principalTable: "contract_Pops",
                principalColumns: new[] { "ContractID", "PopID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
