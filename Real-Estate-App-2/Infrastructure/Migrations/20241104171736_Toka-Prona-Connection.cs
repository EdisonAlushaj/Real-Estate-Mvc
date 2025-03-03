using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class TokaPronaConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokat",
                columns: table => new
                {
                    PronaID = table.Column<int>(type: "int", nullable: false),
                    LandType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopografiaTokes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterSource = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokat", x => x.PronaID);
                    table.ForeignKey(
                        name: "FK_Tokat_Pronas_PronaID",
                        column: x => x.PronaID,
                        principalTable: "Pronas",
                        principalColumn: "PronaID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokat");
        }
    }
}
