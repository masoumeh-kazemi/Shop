using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addHasKeyToUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "user",
                table: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "user",
                table: "Roles",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "user",
                table: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "user",
                table: "Roles",
                columns: new[] { "UserId", "Id" });
        }
    }
}
