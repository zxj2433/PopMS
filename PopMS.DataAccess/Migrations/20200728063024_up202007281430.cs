using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007281430 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contract_Pops_ContractID",
                table: "contract_Pops");

            migrationBuilder.CreateIndex(
                name: "IX_contract_Pops_ContractID_PopID",
                table: "contract_Pops",
                columns: new[] { "ContractID", "PopID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contract_Pops_ContractID_PopID",
                table: "contract_Pops");

            migrationBuilder.CreateIndex(
                name: "IX_contract_Pops_ContractID",
                table: "contract_Pops",
                column: "ContractID");
        }
    }
}
