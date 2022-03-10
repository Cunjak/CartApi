using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class checkConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE [dbo].[Cart]
                ADD CONSTRAINT CK_Status CHECK ([Status]='Submitted' OR [Status]='Cancelled' OR [Status]='Draft');");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Standard",
                column: "ConcurrencyStamp",
                value: "b6e8c5b9-d638-41b5-8682-40f6e17c0750");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Viewer",
                column: "ConcurrencyStamp",
                value: "b348b065-9e58-45b1-8f8e-26083a504a73");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbe2b9eb-4421-417c-a0a7-c3c8a3306b49", "AQAAAAEAACcQAAAAEIxwbGj8PGjudIg7IVEEaHIfl0WCN1m+uITG9on/IWV4WW75hBF0Qsua6FmpEi8Oow==", "8b8cbbd9-24ef-4bf3-a1d9-fdb828d22d42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "676660f2-ba1e-4851-add6-1f3c6c5bf9f8", "AQAAAAEAACcQAAAAEBZyaQDAkxGM5Bnt6gTNOEtRlihFakoBxmH6MY314MKKmvV28+nlwrGMGkkcT4NFlQ==", "854fc757-ecb0-421c-b204-e87152118166" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
