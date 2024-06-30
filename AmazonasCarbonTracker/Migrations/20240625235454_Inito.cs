using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AmazonasCarbonTracker.Migrations
{
    /// <inheritdoc />
    public partial class Inito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1602490d-5342-4009-99c5-1c3f2c337092");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e6dbea7-16a1-4094-8239-41f3b77e7aa3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3a6d38f-f002-4043-b56f-b068c57cde64");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30560e83-519c-47fe-90ec-4d720d0bb54c", null, "Company", "COMPANY" },
                    { "82a0bbdf-41ed-4d2b-b61d-68c2a327fead", null, "User", "USER" },
                    { "bdaea35a-1261-4065-8a0c-2a4347db19e3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30560e83-519c-47fe-90ec-4d720d0bb54c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82a0bbdf-41ed-4d2b-b61d-68c2a327fead");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdaea35a-1261-4065-8a0c-2a4347db19e3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1602490d-5342-4009-99c5-1c3f2c337092", null, "Company", "COMPANY" },
                    { "1e6dbea7-16a1-4094-8239-41f3b77e7aa3", null, "User", "USER" },
                    { "d3a6d38f-f002-4043-b56f-b068c57cde64", null, "Admin", "ADMIN" }
                });
        }
    }
}
