using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.DAL.Migrations
{
    public partial class FixedReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3a8a65be-f175-42a0-a3c0-68a9a10ef666", "33463803-0f64-458c-bd02-b43c73399d1a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "87b15c22-4d1d-4e2d-99ce-ad2796f1a600", "26fffd5c-d2c4-436e-a589-b115cc2eb283" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e77cbef-1077-4b83-9682-7638f2efce8e", "72415d5b-7472-4fa5-864f-53668f17948f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6736f32-93a2-484f-9464-268c779befd2", "2fbf5a08-942c-436e-8133-6803520f975f", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7e77cbef-1077-4b83-9682-7638f2efce8e", "72415d5b-7472-4fa5-864f-53668f17948f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a6736f32-93a2-484f-9464-268c779befd2", "2fbf5a08-942c-436e-8133-6803520f975f" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87b15c22-4d1d-4e2d-99ce-ad2796f1a600", "26fffd5c-d2c4-436e-a589-b115cc2eb283", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a8a65be-f175-42a0-a3c0-68a9a10ef666", "33463803-0f64-458c-bd02-b43c73399d1a", "User", "USER" });
        }
    }
}
