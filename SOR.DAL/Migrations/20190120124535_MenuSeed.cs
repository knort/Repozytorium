using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.DAL.Migrations
{
    public partial class MenuSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6baa6b36-eb6a-4587-b2e1-c1c2531336c3", "bb8a5c39-19d6-4f2e-b237-6559e83e596b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e6862f65-eced-46bf-b8aa-ebff0f6f994c", "0240cadf-24e7-4a76-888e-e36046e6444a" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c373eed-8184-46f1-a811-6d5b1a9e7b78", "14a17a34-0a0e-4134-ab70-9ea6f3f8c5e6", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "181f56d5-7afc-4cdb-b6c9-eba090296345", "2dfa01c9-b159-4039-842e-ffceb1c1cc12", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "Title" },
                values: new object[] { new Guid("0c1071d0-d155-4a4f-a7c8-699d885f0107"), "Karta restauracji" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "181f56d5-7afc-4cdb-b6c9-eba090296345", "2dfa01c9-b159-4039-842e-ffceb1c1cc12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7c373eed-8184-46f1-a811-6d5b1a9e7b78", "14a17a34-0a0e-4134-ab70-9ea6f3f8c5e6" });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("0c1071d0-d155-4a4f-a7c8-699d885f0107"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6862f65-eced-46bf-b8aa-ebff0f6f994c", "0240cadf-24e7-4a76-888e-e36046e6444a", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6baa6b36-eb6a-4587-b2e1-c1c2531336c3", "bb8a5c39-19d6-4f2e-b237-6559e83e596b", "User", "USER" });
        }
    }
}
