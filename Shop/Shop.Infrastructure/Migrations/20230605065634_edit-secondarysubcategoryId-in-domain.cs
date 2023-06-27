using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editsecondarysubcategoryIdindomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategroyId",
                schema: "product",
                table: "Products",
                newName: "SubCategoryId");

            migrationBuilder.RenameColumn(
                name: "SecondarySubCategroyId",
                schema: "product",
                table: "Products",
                newName: "SecondarySubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                schema: "product",
                table: "Products",
                newName: "SubCategroyId");

            migrationBuilder.RenameColumn(
                name: "SecondarySubCategoryId",
                schema: "product",
                table: "Products",
                newName: "SecondarySubCategroyId");
        }
    }
}
