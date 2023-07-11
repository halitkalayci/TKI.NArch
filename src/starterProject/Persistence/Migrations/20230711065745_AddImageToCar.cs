using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 11, 6, 57, 44, 991, DateTimeKind.Utc).AddTicks(9068), new byte[] { 184, 212, 244, 60, 111, 88, 209, 225, 179, 78, 9, 21, 153, 105, 241, 172, 45, 129, 27, 16, 122, 225, 157, 192, 159, 215, 230, 103, 161, 103, 240, 158, 251, 86, 216, 8, 57, 101, 219, 148, 59, 170, 150, 16, 40, 75, 125, 69, 113, 69, 62, 15, 226, 0, 242, 73, 20, 91, 241, 235, 179, 204, 102, 166 }, new byte[] { 51, 160, 145, 78, 135, 201, 222, 144, 253, 154, 235, 59, 225, 28, 128, 112, 171, 42, 220, 169, 115, 49, 187, 156, 149, 207, 51, 250, 219, 5, 181, 112, 182, 245, 152, 81, 65, 231, 249, 212, 44, 68, 234, 164, 36, 130, 235, 9, 35, 66, 19, 220, 215, 204, 147, 244, 151, 22, 253, 235, 121, 47, 181, 121, 22, 127, 189, 28, 230, 86, 29, 229, 133, 129, 42, 62, 131, 143, 27, 209, 16, 144, 179, 86, 114, 159, 236, 173, 193, 253, 62, 170, 236, 96, 81, 228, 240, 167, 220, 163, 110, 159, 230, 217, 246, 168, 201, 59, 106, 11, 141, 103, 226, 59, 9, 101, 18, 48, 171, 212, 20, 229, 55, 41, 246, 62, 67, 250 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 3, 11, 36, 45, 924, DateTimeKind.Utc).AddTicks(1625), new byte[] { 215, 188, 196, 246, 83, 218, 17, 139, 210, 209, 110, 175, 157, 202, 33, 178, 121, 134, 121, 45, 220, 213, 154, 94, 43, 136, 235, 140, 189, 112, 114, 204, 1, 255, 123, 96, 222, 97, 157, 165, 155, 241, 190, 65, 149, 152, 40, 51, 101, 11, 184, 188, 69, 236, 10, 241, 120, 4, 153, 31, 171, 229, 3, 190 }, new byte[] { 115, 0, 83, 127, 236, 128, 57, 64, 105, 71, 196, 156, 225, 66, 145, 21, 62, 244, 133, 18, 1, 186, 231, 111, 139, 79, 30, 0, 30, 192, 249, 130, 157, 9, 206, 113, 83, 67, 222, 116, 20, 147, 21, 148, 209, 246, 41, 42, 86, 254, 135, 180, 168, 18, 227, 112, 201, 27, 84, 251, 138, 255, 67, 119, 138, 44, 87, 159, 113, 0, 151, 62, 194, 224, 103, 173, 207, 149, 129, 141, 23, 21, 190, 170, 30, 1, 125, 145, 96, 44, 231, 195, 19, 33, 38, 234, 41, 164, 107, 226, 161, 58, 59, 61, 128, 69, 48, 212, 49, 246, 76, 66, 42, 251, 2, 156, 152, 212, 82, 15, 97, 5, 45, 30, 60, 24, 111, 141 } });
        }
    }
}
