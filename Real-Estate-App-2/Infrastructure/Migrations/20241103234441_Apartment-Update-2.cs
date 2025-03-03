using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    PronaID = table.Column<int>(type: "int", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    nrDhomave = table.Column<int>(type: "int", nullable: false),
                    kaAnshensor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.PronaID);
                    table.ForeignKey(
                        name: "FK_Apartments_Pronas_PronaID",
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
    }
}
