using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AmazonasCarbonTracker.Migrations
{
    /// <inheritdoc />
    public partial class Initoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmissionRecord_AspNetUsers_AppUserId1",
                table: "EmissionRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_SustainabilityMetrics_AspNetUsers_AppUserId1",
                table: "SustainabilityMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SustainabilityMetrics",
                table: "SustainabilityMetrics");

            migrationBuilder.DropIndex(
                name: "IX_SustainabilityMetrics_AppUserId1",
                table: "SustainabilityMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmissionRecord",
                table: "EmissionRecord");

            migrationBuilder.DropIndex(
                name: "IX_EmissionRecord_AppUserId1",
                table: "EmissionRecord");

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

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "SustainabilityMetrics");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "EmissionRecord");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SustainabilityMetrics",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "SustainabilityMetrics",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmissionRecord",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "EmissionRecord",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SustainabilityMetrics",
                table: "SustainabilityMetrics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmissionRecord",
                table: "EmissionRecord",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55f1b04c-0ca1-4d48-95d3-082be45b2e03", null, "Company", "COMPANY" },
                    { "812ba948-7c39-4adf-a4e1-fa865ddb7984", null, "User", "USER" },
                    { "889d8e98-884c-4496-9543-64b7b4358fb1", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SustainabilityMetrics_AppUserId",
                table: "SustainabilityMetrics",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmissionRecord_AppUserId",
                table: "EmissionRecord",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmissionRecord_AspNetUsers_AppUserId",
                table: "EmissionRecord",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SustainabilityMetrics_AspNetUsers_AppUserId",
                table: "SustainabilityMetrics",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmissionRecord_AspNetUsers_AppUserId",
                table: "EmissionRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_SustainabilityMetrics_AspNetUsers_AppUserId",
                table: "SustainabilityMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SustainabilityMetrics",
                table: "SustainabilityMetrics");

            migrationBuilder.DropIndex(
                name: "IX_SustainabilityMetrics_AppUserId",
                table: "SustainabilityMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmissionRecord",
                table: "EmissionRecord");

            migrationBuilder.DropIndex(
                name: "IX_EmissionRecord_AppUserId",
                table: "EmissionRecord");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55f1b04c-0ca1-4d48-95d3-082be45b2e03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "812ba948-7c39-4adf-a4e1-fa865ddb7984");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "889d8e98-884c-4496-9543-64b7b4358fb1");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "SustainabilityMetrics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SustainabilityMetrics",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "SustainabilityMetrics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "EmissionRecord",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmissionRecord",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "EmissionRecord",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SustainabilityMetrics",
                table: "SustainabilityMetrics",
                column: "AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmissionRecord",
                table: "EmissionRecord",
                column: "AppUserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30560e83-519c-47fe-90ec-4d720d0bb54c", null, "Company", "COMPANY" },
                    { "82a0bbdf-41ed-4d2b-b61d-68c2a327fead", null, "User", "USER" },
                    { "bdaea35a-1261-4065-8a0c-2a4347db19e3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SustainabilityMetrics_AppUserId1",
                table: "SustainabilityMetrics",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmissionRecord_AppUserId1",
                table: "EmissionRecord",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EmissionRecord_AspNetUsers_AppUserId1",
                table: "EmissionRecord",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SustainabilityMetrics_AspNetUsers_AppUserId1",
                table: "SustainabilityMetrics",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
