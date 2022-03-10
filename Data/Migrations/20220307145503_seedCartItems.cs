using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class seedCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Standard",
                column: "ConcurrencyStamp",
                value: "92deeab4-b9b0-4719-8919-991566df4898");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Viewer",
                column: "ConcurrencyStamp",
                value: "88e27b77-3d2d-40ac-83e4-10ee2b556f6d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b9523ee-dd3a-42b8-9ec2-b4b5eee4cf61", "AQAAAAEAACcQAAAAELEirzz4/jPo/14xJg6Bro3So4licRVJ2/q3R2jtj6F4xcIcg7ewHUcD9BO4dfJsRQ==", "bef2249a-b527-4fda-8bf6-7b098f19f2de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f542a57-5451-41bd-9835-15e2d2126dd9", "AQAAAAEAACcQAAAAEBOLEP7zaQjW86UzrVkHtUWyQeaepoTO2Q1rHQjaD+cY7Od9PKu/KN5N7WeSX6KkHA==", "c7296e1a-4da1-4f5e-9b8d-b94a594abad1" });

            migrationBuilder.InsertData(
                table: "CartItem",
                columns: new[] { "Id", "CartId", "CreatedBy", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Aleksandar", "Dell Vostro 3500 256 GB SSD", "Laptop" },
                    { 2, 1, "Aleksandar", "Xiaomi Redmi 6A", "Phone" },
                    { 3, 1, "Aleksandar", "Xiaomi Mi 10", "Phone" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CartItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CartItem",
                keyColumn: "Id",
                keyValue: 3);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e6f539e-4341-448a-89c9-34bb5d0345f6", "AQAAAAEAACcQAAAAEMjvyntkYmh73Py+E0RajxqdZw1e1vIcDoF5LXBdyqRD7Lo3zwjnrNI2J/v768s3JA==", "e2e22611-3fa1-496b-8dfb-70dce7512e63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8942c56-298d-4554-8225-0a872ce65e0f", "AQAAAAEAACcQAAAAECZ3Qj3gO1tLqwniimxn3Gntyj5z75LuE2x8jdw1YnVKMHn10VndHZCsFywwrT5R0g==", "7cc18b6e-7947-41f5-8293-80634aeb16c4" });
        }
    }
}
