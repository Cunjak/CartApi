using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class seedCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Standard",
                column: "ConcurrencyStamp",
                value: "5ff60135-b11d-4f74-8258-897b5493b50b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Viewer",
                column: "ConcurrencyStamp",
                value: "c2ae488f-57fb-4d58-9ba2-10ad1d103381");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e6f539e-4341-448a-89c9-34bb5d0345f6", "ALEKSANDAR", "AQAAAAEAACcQAAAAEMjvyntkYmh73Py+E0RajxqdZw1e1vIcDoF5LXBdyqRD7Lo3zwjnrNI2J/v768s3JA==", "e2e22611-3fa1-496b-8dfb-70dce7512e63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8942c56-298d-4554-8225-0a872ce65e0f", "MILOS", "AQAAAAEAACcQAAAAECZ3Qj3gO1tLqwniimxn3Gntyj5z75LuE2x8jdw1YnVKMHn10VndHZCsFywwrT5R0g==", "7cc18b6e-7947-41f5-8293-80634aeb16c4" });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedBy", "Status" },
                values: new object[] { 1, "Aleksandar", "Draft" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: 1);

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e981a7e7-a090-472e-8944-e8a1aff8a05f", null, "AQAAAAEAACcQAAAAELKBgHq+/9c/MvHMjyiwMODv7S0S8OYMF3iMhmaHLa4q9KvNOCRLMU6UiBT+OXXUug==", "1af85519-ae4c-4cde-a316-5c138ca30f33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfb0b3ff-5f5e-465a-8761-d1f68c25764b", null, "AQAAAAEAACcQAAAAEDj6MSQnPB++TV+cui2oMLXlahgrTVMypQ3Q5GuITcFO9msmjDxXRe2Ylu+UPnqn9g==", "91711a45-b185-4bea-a043-20d8215377dc" });
        }
    }
}
