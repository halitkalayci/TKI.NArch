using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updaterental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Rentals",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 21, 11, 41, 54, 81, DateTimeKind.Utc).AddTicks(2327), new byte[] { 7, 231, 21, 221, 95, 192, 136, 206, 127, 139, 211, 59, 76, 251, 181, 150, 158, 197, 240, 111, 118, 94, 173, 236, 134, 253, 9, 102, 20, 231, 6, 113, 47, 221, 117, 111, 133, 218, 13, 198, 73, 41, 182, 136, 19, 230, 189, 88, 250, 180, 205, 7, 24, 252, 39, 71, 251, 29, 107, 67, 92, 62, 50, 8 }, new byte[] { 185, 129, 8, 199, 89, 9, 114, 231, 95, 128, 156, 210, 142, 230, 184, 186, 87, 189, 110, 141, 72, 75, 66, 196, 175, 131, 0, 153, 13, 74, 110, 143, 9, 160, 49, 141, 243, 249, 92, 48, 63, 234, 239, 219, 33, 158, 146, 220, 219, 19, 100, 99, 204, 179, 237, 153, 55, 87, 182, 50, 198, 200, 197, 221, 254, 132, 84, 15, 54, 66, 90, 47, 255, 117, 252, 176, 173, 214, 67, 230, 199, 65, 147, 167, 207, 22, 79, 146, 93, 151, 214, 60, 32, 92, 20, 13, 100, 117, 125, 76, 124, 212, 246, 209, 42, 35, 37, 236, 242, 9, 120, 211, 193, 24, 210, 209, 17, 183, 247, 18, 166, 34, 234, 43, 240, 44, 19, 83 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rentals");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 21, 10, 45, 44, 659, DateTimeKind.Utc).AddTicks(5691), new byte[] { 250, 167, 188, 145, 28, 8, 119, 50, 169, 180, 22, 112, 231, 42, 64, 63, 139, 48, 96, 251, 92, 152, 38, 61, 5, 254, 53, 192, 200, 136, 246, 210, 248, 246, 246, 244, 138, 201, 18, 39, 118, 238, 79, 18, 50, 214, 106, 235, 39, 145, 104, 119, 189, 6, 80, 42, 2, 12, 214, 92, 188, 125, 168, 113 }, new byte[] { 238, 69, 35, 250, 240, 82, 160, 179, 56, 30, 168, 95, 226, 13, 141, 157, 0, 105, 186, 243, 240, 228, 5, 210, 191, 255, 112, 62, 199, 238, 246, 201, 195, 13, 35, 120, 172, 4, 72, 207, 11, 96, 206, 177, 130, 58, 43, 78, 52, 28, 143, 58, 238, 225, 208, 152, 26, 239, 141, 128, 187, 214, 251, 71, 120, 49, 228, 150, 249, 66, 70, 179, 188, 111, 124, 157, 102, 211, 74, 106, 140, 14, 186, 253, 217, 53, 161, 216, 50, 62, 80, 203, 115, 109, 128, 69, 176, 215, 136, 36, 209, 9, 149, 158, 181, 183, 31, 200, 78, 180, 204, 100, 113, 162, 181, 127, 29, 100, 61, 211, 223, 63, 112, 35, 86, 144, 219, 111 } });
        }
    }
}
