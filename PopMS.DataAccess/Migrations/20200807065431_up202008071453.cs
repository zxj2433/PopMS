using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202008071453 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contracts_ContractID",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_contracts_DCID",
                table: "contracts");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_DCID_ContractID",
                table: "contracts",
                columns: new[] { "DCID", "ContractID" },
                unique: true,
                filter: "[ContractID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contracts_DCID_ContractID",
                table: "contracts");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_ContractID",
                table: "contracts",
                column: "ContractID",
                unique: true,
                filter: "[ContractID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_DCID",
                table: "contracts",
                column: "DCID");
        }
    }
}
