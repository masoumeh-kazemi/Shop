using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hasKeyRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                schema: "role",
                table: "Permissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                schema: "role",
                table: "Permissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                schema: "role",
                table: "Permissions",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                schema: "role",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RoleId",
                schema: "role",
                table: "Permissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                schema: "role",
                table: "Permissions",
                columns: new[] { "RoleId", "Id" });
        }
    }
}
