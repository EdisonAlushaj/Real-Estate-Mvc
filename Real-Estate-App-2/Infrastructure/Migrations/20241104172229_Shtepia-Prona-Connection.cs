using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class ShtepiaPronaConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shtepiat",
                columns: table => new
                {
                    PronaID = table.Column<int>(type: "int", nullable: false),
                    size = table.Column<double>(type: "float", nullable: false),
                    nrFloors = table.Column<int>(type: "int", nullable: false),
                    kaGarazhd = table.Column<bool>(type: "bit", nullable: false),
                    kaPool = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shtepiat", x => x.PronaID);
                    table.ForeignKey(
                        name: "FK_Shtepiat_Pronas_PronaID",
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
                name: "Shtepiat");
        }
    }
}
