using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalWebProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResellerImportReceiptDetail",
                table: "ResellerImportReceiptDetail");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "ResellerImportReceiptDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResellerImportReceiptDetail",
                table: "ResellerImportReceiptDetail",
                columns: new[] { "PhoneId", "ResellerImportReceiptId", "WarehouseId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResellerImportReceiptDetail_WarehouseId",
                table: "ResellerImportReceiptDetail",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResellerImportReceiptDetail_Warehouse_WarehouseId",
                table: "ResellerImportReceiptDetail",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResellerImportReceiptDetail_Warehouse_WarehouseId",
                table: "ResellerImportReceiptDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResellerImportReceiptDetail",
                table: "ResellerImportReceiptDetail");

            migrationBuilder.DropIndex(
                name: "IX_ResellerImportReceiptDetail_WarehouseId",
                table: "ResellerImportReceiptDetail");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "ResellerImportReceiptDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResellerImportReceiptDetail",
                table: "ResellerImportReceiptDetail",
                columns: new[] { "PhoneId", "ResellerImportReceiptId" });
        }
    }
}
