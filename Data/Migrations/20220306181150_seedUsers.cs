using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class seedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Standard",
                column: "ConcurrencyStamp",
                value: "0cf5388f-32ca-4e2b-be31-d183da1d63ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Viewer",
                column: "ConcurrencyStamp",
                value: "f1a6b6bf-9c0b-4726-9305-b12638aca99b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "428e7368-384a-4f0a-a8f3-112fbbe43ae7", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEDhyLS+SdDYVeE/EDL69R0iykH6gQTGihZtD4hUL/D2KlAJj+Pf7DV+aruORrtj5mQ==", null, false, "38bd014e-28e3-48ae-8670-57c30fbc3385", false, "Aleksandar" },
                    { "2", 0, "84c2e92a-c3da-43bd-b365-0b4043475f59", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEFQu0jXk/qyZ1CvH8ZtoKWG73wpdsZKNcx0sCcQFWGqWGQ0ehd+H6AgaJChDeMj9Ww==", null, false, "cd9fe98f-634e-4b40-97f5-f42fa3089b4b", false, "Milos" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Standard",
                column: "ConcurrencyStamp",
                value: "c7cf4213-d59f-4147-9a66-5a8144a63b91");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Viewer",
                column: "ConcurrencyStamp",
                value: "c967996a-0e05-4fe8-94f4-afcaa514456e");
        }
    }
}
