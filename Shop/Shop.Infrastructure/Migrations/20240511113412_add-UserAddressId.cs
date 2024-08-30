using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserAddressId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "user",
                table: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "user",
                table: "Addresses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "user",
                table: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "user",
                table: "Addresses",
                columns: new[] { "UserId", "Id" });
        }
    }
}
