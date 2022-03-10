using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class seedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Standard",
                column: "ConcurrencyStamp",
                value: "03f1c405-b249-47fb-bc6e-3a7d097dac30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Viewer",
                column: "ConcurrencyStamp",
                value: "e38b8210-e6d0-4870-ad28-932ba4270142");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "Standard", "1" },
                    { "Viewer", "2" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e981a7e7-a090-472e-8944-e8a1aff8a05f", "AQAAAAEAACcQAAAAELKBgHq+/9c/MvHMjyiwMODv7S0S8OYMF3iMhmaHLa4q9KvNOCRLMU6UiBT+OXXUug==", "1af85519-ae4c-4cde-a316-5c138ca30f33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfb0b3ff-5f5e-465a-8761-d1f68c25764b", "AQAAAAEAACcQAAAAEDj6MSQnPB++TV+cui2oMLXlahgrTVMypQ3Q5GuITcFO9msmjDxXRe2Ylu+UPnqn9g==", "91711a45-b185-4bea-a043-20d8215377dc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "Standard", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "Viewer", "2" });

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "428e7368-384a-4f0a-a8f3-112fbbe43ae7", "AQAAAAEAACcQAAAAEDhyLS+SdDYVeE/EDL69R0iykH6gQTGihZtD4hUL/D2KlAJj+Pf7DV+aruORrtj5mQ==", "38bd014e-28e3-48ae-8670-57c30fbc3385" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84c2e92a-c3da-43bd-b365-0b4043475f59", "AQAAAAEAACcQAAAAEFQu0jXk/qyZ1CvH8ZtoKWG73wpdsZKNcx0sCcQFWGqWGQ0ehd+H6AgaJChDeMj9Ww==", "cd9fe98f-634e-4b40-97f5-f42fa3089b4b" });
        }
    }
}
