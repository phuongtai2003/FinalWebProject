using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalWebProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResellerLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryStatus",
                columns: table => new
                {
                    DeliveryStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatus", x => x.DeliveryStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ExportReceipt",
                columns: table => new
                {
                    ExportReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReceipt", x => x.ExportReceiptId);
                    table.ForeignKey(
                        name: "FK_ExportReceipt_Accountant_AccountantId",
                        column: x => x.AccountantId,
                        principalTable: "Accountant",
                        principalColumn: "AccountantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResellerStorage",
                columns: table => new
                {
                    ResellerId = table.Column<int>(type: "int", nullable: false),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResellerStorage", x => new { x.PhoneId, x.ResellerId });
                    table.ForeignKey(
                        name: "FK_ResellerStorage_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phone",
                        principalColumn: "PhoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResellerStorage_Reseller_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Reseller",
                        principalColumn: "ResellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResellerImportReceipt",
                columns: table => new
                {
                    ResellerImportReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DeliveryStatusId = table.Column<int>(type: "int", nullable: false),
                    ResellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResellerImportReceipt", x => x.ResellerImportReceiptId);
                    table.ForeignKey(
                        name: "FK_ResellerImportReceipt_DeliveryStatus_DeliveryStatusId",
                        column: x => x.DeliveryStatusId,
                        principalTable: "DeliveryStatus",
                        principalColumn: "DeliveryStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResellerImportReceipt_Reseller_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Reseller",
                        principalColumn: "ResellerId");
                });

            migrationBuilder.CreateTable(
                name: "ExportReceiptDetails",
                columns: table => new
                {
                    ResellerId = table.Column<int>(type: "int", nullable: false),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    ExportReceiptId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReceiptDetails", x => new { x.PhoneId, x.ResellerId, x.ExportReceiptId });
                    table.ForeignKey(
                        name: "FK_ExportReceiptDetails_ExportReceipt_ExportReceiptId",
                        column: x => x.ExportReceiptId,
                        principalTable: "ExportReceipt",
                        principalColumn: "ExportReceiptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportReceiptDetails_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phone",
                        principalColumn: "PhoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportReceiptDetails_Reseller_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Reseller",
                        principalColumn: "ResellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResellerImportReceiptDetail",
                columns: table => new
                {
                    ResellerImportReceiptId = table.Column<int>(type: "int", nullable: false),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResellerImportReceiptDetail", x => new { x.PhoneId, x.ResellerImportReceiptId });
                    table.ForeignKey(
                        name: "FK_ResellerImportReceiptDetail_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phone",
                        principalColumn: "PhoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResellerImportReceiptDetail_ResellerImportReceipt_ResellerImportReceiptId",
                        column: x => x.ResellerImportReceiptId,
                        principalTable: "ResellerImportReceipt",
                        principalColumn: "ResellerImportReceiptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportReceipt_AccountantId",
                table: "ExportReceipt",
                column: "AccountantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReceiptDetails_ExportReceiptId",
                table: "ExportReceiptDetails",
                column: "ExportReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReceiptDetails_ResellerId",
                table: "ExportReceiptDetails",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerImportReceipt_DeliveryStatusId",
                table: "ResellerImportReceipt",
                column: "DeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerImportReceipt_ResellerId",
                table: "ResellerImportReceipt",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerImportReceiptDetail_ResellerImportReceiptId",
                table: "ResellerImportReceiptDetail",
                column: "ResellerImportReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerStorage_ResellerId",
                table: "ResellerStorage",
                column: "ResellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportReceiptDetails");

            migrationBuilder.DropTable(
                name: "ResellerImportReceiptDetail");

            migrationBuilder.DropTable(
                name: "ResellerStorage");

            migrationBuilder.DropTable(
                name: "ExportReceipt");

            migrationBuilder.DropTable(
                name: "ResellerImportReceipt");

            migrationBuilder.DropTable(
                name: "DeliveryStatus");
        }
    }
}
