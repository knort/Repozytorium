using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.DAL.Migrations
{
    public partial class MenuFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Menus_MenuId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_MenuId",
                table: "Foods");

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

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "MenuFoods",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(nullable: false),
                    FoodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoods", x => new { x.MenuId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_MenuFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoods_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoods_FoodId",
                table: "MenuFoods",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuFoods");

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
                name: "MenuId",
                table: "Foods",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Foods_MenuId",
                table: "Foods",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Menus_MenuId",
                table: "Foods",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
