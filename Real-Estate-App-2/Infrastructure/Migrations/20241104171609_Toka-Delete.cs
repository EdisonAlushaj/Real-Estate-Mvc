using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class TokaDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokat",
                columns: table => new
                {
                    TokaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopografiaTokes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterSource = table.Column<bool>(type: "bit", nullable: false),
                    Zona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokat", x => x.TokaID);
                });
        }
    }
}
