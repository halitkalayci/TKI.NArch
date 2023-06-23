using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBrandFromModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId1",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Models");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 6, 23, 11, 26, 57, 521, DateTimeKind.Utc).AddTicks(2503), new byte[] { 51, 12, 3, 84, 10, 97, 80, 232, 90, 37, 202, 92, 78, 173, 134, 228, 156, 105, 227, 111, 177, 102, 38, 160, 36, 130, 18, 18, 30, 245, 158, 42, 7, 238, 197, 133, 236, 88, 70, 166, 151, 167, 77, 97, 156, 80, 221, 199, 238, 213, 223, 229, 40, 177, 69, 72, 169, 215, 246, 9, 14, 76, 255, 49 }, new byte[] { 113, 109, 248, 247, 6, 17, 140, 125, 81, 153, 157, 142, 181, 245, 253, 171, 138, 248, 239, 222, 141, 220, 140, 126, 169, 45, 16, 174, 255, 44, 56, 156, 149, 218, 84, 88, 14, 109, 89, 193, 47, 163, 183, 20, 148, 13, 60, 71, 38, 129, 51, 76, 144, 118, 123, 41, 190, 226, 112, 204, 43, 98, 189, 189, 182, 1, 1, 147, 166, 186, 87, 202, 30, 193, 7, 6, 197, 181, 199, 149, 80, 94, 75, 223, 147, 13, 125, 87, 206, 16, 2, 130, 23, 205, 175, 163, 193, 28, 92, 47, 182, 44, 227, 18, 123, 250, 156, 99, 173, 125, 46, 0, 172, 253, 58, 157, 23, 33, 19, 75, 34, 106, 175, 100, 126, 252, 138, 12 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Models",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "BrandId1",
                table: "Models",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 6, 23, 8, 7, 3, 594, DateTimeKind.Utc).AddTicks(9920), new byte[] { 101, 168, 103, 46, 17, 151, 252, 104, 112, 93, 214, 191, 10, 148, 223, 227, 156, 6, 202, 166, 170, 138, 125, 131, 204, 75, 143, 251, 111, 4, 12, 63, 157, 54, 250, 183, 154, 107, 33, 96, 3, 145, 98, 71, 118, 120, 135, 96, 89, 53, 208, 181, 247, 101, 142, 119, 12, 176, 161, 205, 239, 91, 83, 152 }, new byte[] { 220, 33, 140, 209, 31, 8, 208, 192, 106, 148, 15, 205, 209, 127, 251, 253, 71, 80, 14, 47, 11, 34, 107, 39, 168, 213, 104, 100, 240, 196, 81, 175, 66, 3, 83, 86, 220, 164, 180, 150, 248, 95, 177, 75, 182, 0, 221, 246, 101, 138, 126, 210, 108, 114, 88, 141, 113, 78, 84, 68, 180, 102, 107, 195, 218, 148, 197, 77, 146, 89, 40, 246, 232, 8, 179, 236, 133, 196, 92, 231, 181, 11, 194, 86, 54, 184, 163, 158, 5, 129, 150, 124, 154, 212, 70, 226, 194, 64, 33, 84, 64, 105, 213, 4, 66, 111, 50, 68, 135, 53, 151, 141, 148, 139, 87, 245, 127, 144, 216, 196, 101, 217, 122, 67, 27, 100, 55, 188 } });

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId1",
                table: "Models",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
