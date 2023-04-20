using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalWebProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "Accountant",
                columns: table => new
                {
                    AccountantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcccountantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountantEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountantPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accountant", x => x.AccountantID);
                    table.ForeignKey(
                        name: "FK_Accountant_Warehouse_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accountant_WarehouseID",
                table: "Accountant",
                column: "WarehouseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accountant");

            migrationBuilder.DropTable(
                name: "Warehouse");
        }
    }
}
