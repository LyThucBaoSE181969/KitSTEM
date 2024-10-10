using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SWP.KitStem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69b0258a-f65f-4fab-a871-cdd3a3dca897", "4", "customer", "CUSTOMER" },
                    { "b94d56fc-5c28-4065-bd6f-50642942c318", "1", "admin", "ADMIN" },
                    { "c3251f5b-d52a-44a7-a49f-92d24ca0e7d5", "2", "manager", "MANAGER" },
                    { "e8d4ab81-6f7d-4b93-8642-f99b6c8154f0", "3", "staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69b0258a-f65f-4fab-a871-cdd3a3dca897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b94d56fc-5c28-4065-bd6f-50642942c318");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3251f5b-d52a-44a7-a49f-92d24ca0e7d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8d4ab81-6f7d-4b93-8642-f99b6c8154f0");
        }
    }
}
    