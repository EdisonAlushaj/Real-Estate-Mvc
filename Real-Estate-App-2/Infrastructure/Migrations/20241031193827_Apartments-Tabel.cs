using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentsTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Properties",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "floor",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "kaAnshensor",
                table: "Properties",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nrDhomave",
                table: "Properties",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "floor",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "kaAnshensor",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "nrDhomave",
                table: "Properties");
        }
    }
}
