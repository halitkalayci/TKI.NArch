using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatecar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DailyPrice",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 21, 10, 45, 44, 659, DateTimeKind.Utc).AddTicks(5691), new byte[] { 250, 167, 188, 145, 28, 8, 119, 50, 169, 180, 22, 112, 231, 42, 64, 63, 139, 48, 96, 251, 92, 152, 38, 61, 5, 254, 53, 192, 200, 136, 246, 210, 248, 246, 246, 244, 138, 201, 18, 39, 118, 238, 79, 18, 50, 214, 106, 235, 39, 145, 104, 119, 189, 6, 80, 42, 2, 12, 214, 92, 188, 125, 168, 113 }, new byte[] { 238, 69, 35, 250, 240, 82, 160, 179, 56, 30, 168, 95, 226, 13, 141, 157, 0, 105, 186, 243, 240, 228, 5, 210, 191, 255, 112, 62, 199, 238, 246, 201, 195, 13, 35, 120, 172, 4, 72, 207, 11, 96, 206, 177, 130, 58, 43, 78, 52, 28, 143, 58, 238, 225, 208, 152, 26, 239, 141, 128, 187, 214, 251, 71, 120, 49, 228, 150, 249, 66, 70, 179, 188, 111, 124, 157, 102, 211, 74, 106, 140, 14, 186, 253, 217, 53, 161, 216, 50, 62, 80, 203, 115, 109, 128, 69, 176, 215, 136, 36, 209, 9, 149, 158, 181, 183, 31, 200, 78, 180, 204, 100, 113, 162, 181, 127, 29, 100, 61, 211, 223, 63, 112, 35, 86, 144, 219, 111 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyPrice",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 19, 10, 55, 12, 228, DateTimeKind.Utc).AddTicks(8447), new byte[] { 147, 215, 74, 216, 137, 206, 230, 182, 199, 106, 51, 188, 146, 111, 101, 159, 145, 239, 131, 148, 171, 160, 193, 118, 225, 14, 114, 153, 212, 87, 98, 226, 75, 229, 38, 226, 76, 246, 98, 49, 27, 252, 97, 250, 201, 188, 167, 68, 29, 100, 13, 146, 78, 157, 137, 48, 80, 54, 182, 106, 19, 199, 127, 183 }, new byte[] { 109, 68, 130, 124, 107, 161, 127, 36, 222, 0, 180, 118, 137, 108, 197, 195, 1, 77, 95, 251, 130, 195, 19, 252, 8, 69, 69, 229, 92, 132, 254, 180, 70, 81, 180, 1, 160, 94, 172, 83, 240, 239, 127, 175, 122, 38, 126, 72, 96, 144, 73, 225, 249, 110, 158, 64, 123, 153, 223, 4, 80, 79, 46, 179, 97, 125, 37, 238, 173, 174, 33, 129, 217, 6, 204, 27, 214, 28, 119, 17, 224, 227, 178, 146, 116, 14, 63, 94, 185, 169, 236, 31, 64, 203, 97, 173, 9, 190, 224, 144, 171, 111, 176, 195, 146, 224, 50, 52, 20, 221, 234, 131, 57, 118, 7, 11, 97, 42, 128, 76, 202, 28, 171, 84, 153, 19, 66, 110 } });
        }
    }
}
