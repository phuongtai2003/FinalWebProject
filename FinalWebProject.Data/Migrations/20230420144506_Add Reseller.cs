using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalWebProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReseller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reseller",
                columns: table => new
                {
                    ResellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResellerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResellerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResellerLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reseller", x => x.ResellerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reseller");
        }
    }
}
