using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class PronaImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Pronas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Pronas");
        }
    }
}
