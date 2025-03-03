using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pronas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "floor",
                table: "Pronas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "kaAnshensor",
                table: "Pronas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nrDhomave",
                table: "Pronas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pronas");

            migrationBuilder.DropColumn(
                name: "floor",
                table: "Pronas");

            migrationBuilder.DropColumn(
                name: "kaAnshensor",
                table: "Pronas");

            migrationBuilder.DropColumn(
                name: "nrDhomave",
                table: "Pronas");

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    ApartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    kaAnshensor = table.Column<bool>(type: "bit", nullable: false),
                    nrDhomave = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentID);
                });
        }
    }
}
