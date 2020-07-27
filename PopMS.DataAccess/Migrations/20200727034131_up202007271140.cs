using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007271140 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryIn_inventories_InvID",
                table: "inventoryIn");

            migrationBuilder.DropForeignKey(
                name: "FK_inventoryIn_order_Pops_OrderPopID",
                table: "inventoryIn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventoryIn",
                table: "inventoryIn");

            migrationBuilder.RenameTable(
                name: "inventoryIn",
                newName: "inventoryIns");

            migrationBuilder.RenameIndex(
                name: "IX_inventoryIn_OrderPopID",
                table: "inventoryIns",
                newName: "IX_inventoryIns_OrderPopID");

            migrationBuilder.RenameIndex(
                name: "IX_inventoryIn_InvID",
                table: "inventoryIns",
                newName: "IX_inventoryIns_InvID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventoryIns",
                table: "inventoryIns",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryIns_inventories_InvID",
                table: "inventoryIns",
                column: "InvID",
                principalTable: "inventories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryIns_order_Pops_OrderPopID",
                table: "inventoryIns",
                column: "OrderPopID",
                principalTable: "order_Pops",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryIns_inventories_InvID",
                table: "inventoryIns");

            migrationBuilder.DropForeignKey(
                name: "FK_inventoryIns_order_Pops_OrderPopID",
                table: "inventoryIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventoryIns",
                table: "inventoryIns");

            migrationBuilder.RenameTable(
                name: "inventoryIns",
                newName: "inventoryIn");

            migrationBuilder.RenameIndex(
                name: "IX_inventoryIns_OrderPopID",
                table: "inventoryIn",
                newName: "IX_inventoryIn_OrderPopID");

            migrationBuilder.RenameIndex(
                name: "IX_inventoryIns_InvID",
                table: "inventoryIn",
                newName: "IX_inventoryIn_InvID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventoryIn",
                table: "inventoryIn",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryIn_inventories_InvID",
                table: "inventoryIn",
                column: "InvID",
                principalTable: "inventories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryIn_order_Pops_OrderPopID",
                table: "inventoryIn",
                column: "OrderPopID",
                principalTable: "order_Pops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
