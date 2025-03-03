using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class DBBugFixs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Electronics_Products_PropertyID",
                table: "Electronics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Electronics",
                table: "Electronics");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Properties");

            migrationBuilder.RenameTable(
                name: "Electronics",
                newName: "Apartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Properties_PropertyID",
                table: "Apartments",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Properties_PropertyID",
                table: "Apartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Electronics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "PropertyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Electronics",
                table: "Electronics",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Electronics_Products_PropertyID",
                table: "Electronics",
                column: "PropertyID",
                principalTable: "Products",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
