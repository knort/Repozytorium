using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.DAL.Migrations
{
    public partial class ReservationID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_SORUserId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7e77cbef-1077-4b83-9682-7638f2efce8e", "72415d5b-7472-4fa5-864f-53668f17948f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a6736f32-93a2-484f-9464-268c779befd2", "2fbf5a08-942c-436e-8133-6803520f975f" });

            migrationBuilder.AlterColumn<string>(
                name: "SORUserId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Reservations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "ReservationId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6862f65-eced-46bf-b8aa-ebff0f6f994c", "0240cadf-24e7-4a76-888e-e36046e6444a", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6baa6b36-eb6a-4587-b2e1-c1c2531336c3", "bb8a5c39-19d6-4f2e-b237-6559e83e596b", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SORUserId",
                table: "Reservations",
                column: "SORUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_SORUserId",
                table: "Reservations",
                column: "SORUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_SORUserId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_SORUserId",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6baa6b36-eb6a-4587-b2e1-c1c2531336c3", "bb8a5c39-19d6-4f2e-b237-6559e83e596b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e6862f65-eced-46bf-b8aa-ebff0f6f994c", "0240cadf-24e7-4a76-888e-e36046e6444a" });

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "SORUserId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "SORUserId", "TableId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e77cbef-1077-4b83-9682-7638f2efce8e", "72415d5b-7472-4fa5-864f-53668f17948f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6736f32-93a2-484f-9464-268c779befd2", "2fbf5a08-942c-436e-8133-6803520f975f", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_SORUserId",
                table: "Reservations",
                column: "SORUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
