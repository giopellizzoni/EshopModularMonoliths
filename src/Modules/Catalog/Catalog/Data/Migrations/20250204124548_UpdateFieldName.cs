#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastModified",
                schema: "catalog",
                table: "Products",
                newName: "LastModified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                schema: "catalog",
                table: "Products",
                newName: "lastModified");
        }
    }
}
