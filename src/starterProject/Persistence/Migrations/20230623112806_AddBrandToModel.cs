using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BrandId",
                table: "Models",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 6, 23, 11, 28, 6, 520, DateTimeKind.Utc).AddTicks(7757), new byte[] { 224, 14, 236, 84, 111, 244, 94, 10, 182, 55, 94, 221, 215, 132, 243, 39, 197, 126, 133, 37, 58, 27, 22, 92, 97, 206, 57, 242, 119, 2, 25, 141, 213, 66, 255, 106, 57, 147, 128, 134, 229, 223, 107, 41, 117, 116, 176, 141, 166, 18, 24, 35, 232, 148, 15, 155, 213, 47, 141, 97, 43, 69, 152, 143 }, new byte[] { 48, 242, 234, 5, 251, 9, 178, 72, 128, 88, 224, 198, 240, 43, 22, 144, 49, 82, 61, 125, 118, 126, 89, 12, 234, 242, 115, 90, 1, 47, 251, 247, 113, 23, 183, 154, 210, 207, 214, 4, 63, 63, 173, 85, 20, 85, 250, 90, 160, 72, 156, 135, 74, 9, 115, 158, 195, 84, 130, 40, 2, 27, 177, 235, 21, 70, 194, 72, 248, 188, 134, 189, 145, 183, 134, 167, 33, 195, 44, 20, 93, 147, 168, 93, 22, 58, 115, 94, 90, 73, 81, 178, 124, 10, 32, 101, 78, 196, 91, 218, 119, 0, 194, 220, 55, 37, 29, 207, 37, 78, 11, 60, 245, 242, 221, 40, 137, 85, 18, 106, 254, 133, 244, 138, 251, 103, 243, 117 } });

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Models");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 6, 23, 11, 26, 57, 521, DateTimeKind.Utc).AddTicks(2503), new byte[] { 51, 12, 3, 84, 10, 97, 80, 232, 90, 37, 202, 92, 78, 173, 134, 228, 156, 105, 227, 111, 177, 102, 38, 160, 36, 130, 18, 18, 30, 245, 158, 42, 7, 238, 197, 133, 236, 88, 70, 166, 151, 167, 77, 97, 156, 80, 221, 199, 238, 213, 223, 229, 40, 177, 69, 72, 169, 215, 246, 9, 14, 76, 255, 49 }, new byte[] { 113, 109, 248, 247, 6, 17, 140, 125, 81, 153, 157, 142, 181, 245, 253, 171, 138, 248, 239, 222, 141, 220, 140, 126, 169, 45, 16, 174, 255, 44, 56, 156, 149, 218, 84, 88, 14, 109, 89, 193, 47, 163, 183, 20, 148, 13, 60, 71, 38, 129, 51, 76, 144, 118, 123, 41, 190, 226, 112, 204, 43, 98, 189, 189, 182, 1, 1, 147, 166, 186, 87, 202, 30, 193, 7, 6, 197, 181, 199, 149, 80, 94, 75, 223, 147, 13, 125, 87, 206, 16, 2, 130, 23, 205, 175, 163, 193, 28, 92, 47, 182, 44, 227, 18, 123, 250, 156, 99, 173, 125, 46, 0, 172, 253, 58, 157, 23, 33, 19, 75, 34, 106, 175, 100, 126, 252, 138, 12 } });
        }
    }
}
