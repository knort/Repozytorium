using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.DAL.Migrations
{
    public partial class MenuFoodFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuFoods",
                table: "MenuFoods");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "34eb5d1d-5c3a-4e3f-95e2-3a49b9c05130", "e74bb28c-5057-47c8-b886-14d1b48669ac" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "85ce3177-4a9e-4ea7-a081-0b5a6d4906b2", "d947bc54-6881-4920-9eba-cc68f9b5d46d" });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("9dccc8fd-850a-458d-bc26-aa8b4da86ff1"));

            migrationBuilder.AddColumn<Guid>(
                name: "MenuFoodId",
                table: "MenuFoods",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuFoods",
                table: "MenuFoods",
                column: "MenuFoodId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "279a87c6-327b-4ac7-9625-f5badab9911d", "591a97d1-5aa6-4cc3-94dd-7d4368576fff", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e072edef-2d89-4549-832c-80420b79e914", "57b9245a-b407-4fa0-ba7a-1f67f88b0751", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "Title" },
                values: new object[] { new Guid("66a622e4-7a74-417b-9478-1838bf7bffe9"), "Karta restauracji" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoods_MenuId",
                table: "MenuFoods",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuFoods",
                table: "MenuFoods");

            migrationBuilder.DropIndex(
                name: "IX_MenuFoods_MenuId",
                table: "MenuFoods");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "279a87c6-327b-4ac7-9625-f5badab9911d", "591a97d1-5aa6-4cc3-94dd-7d4368576fff" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e072edef-2d89-4549-832c-80420b79e914", "57b9245a-b407-4fa0-ba7a-1f67f88b0751" });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("66a622e4-7a74-417b-9478-1838bf7bffe9"));

            migrationBuilder.DropColumn(
                name: "MenuFoodId",
                table: "MenuFoods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuFoods",
                table: "MenuFoods",
                columns: new[] { "MenuId", "FoodId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85ce3177-4a9e-4ea7-a081-0b5a6d4906b2", "d947bc54-6881-4920-9eba-cc68f9b5d46d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34eb5d1d-5c3a-4e3f-95e2-3a49b9c05130", "e74bb28c-5057-47c8-b886-14d1b48669ac", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "Title" },
                values: new object[] { new Guid("9dccc8fd-850a-458d-bc26-aa8b4da86ff1"), "Karta restauracji" });
        }
    }
}
