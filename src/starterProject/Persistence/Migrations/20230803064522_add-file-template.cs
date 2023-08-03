using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addfiletemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileTemplates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 8, 3, 6, 45, 22, 189, DateTimeKind.Utc).AddTicks(196), new byte[] { 120, 122, 240, 206, 237, 105, 79, 96, 117, 18, 82, 42, 231, 85, 32, 212, 19, 173, 199, 255, 56, 16, 59, 143, 137, 39, 7, 102, 153, 115, 189, 36, 235, 163, 107, 187, 39, 231, 99, 66, 116, 249, 86, 50, 158, 206, 164, 15, 3, 228, 115, 227, 243, 77, 205, 169, 178, 26, 186, 29, 204, 249, 135, 118 }, new byte[] { 183, 6, 58, 63, 187, 80, 168, 0, 149, 159, 177, 186, 236, 50, 131, 222, 157, 127, 85, 118, 179, 72, 149, 173, 136, 176, 64, 221, 64, 214, 240, 31, 158, 191, 231, 229, 92, 108, 0, 82, 73, 119, 10, 85, 68, 119, 52, 110, 226, 80, 25, 166, 164, 6, 191, 26, 125, 125, 128, 56, 184, 132, 145, 115, 110, 9, 162, 131, 130, 228, 165, 115, 140, 34, 232, 217, 209, 25, 9, 188, 23, 255, 60, 255, 140, 172, 206, 136, 19, 116, 212, 147, 156, 215, 103, 205, 149, 13, 66, 176, 2, 217, 148, 181, 162, 164, 13, 155, 206, 222, 139, 168, 95, 73, 9, 189, 97, 31, 155, 29, 96, 144, 60, 136, 77, 9, 4, 180 } });

            migrationBuilder.CreateIndex(
                name: "IX_FileTemplates_UserId",
                table: "FileTemplates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileTemplates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 8, 2, 11, 1, 34, 999, DateTimeKind.Utc).AddTicks(3242), new byte[] { 111, 21, 177, 245, 28, 228, 244, 71, 158, 219, 108, 126, 109, 138, 38, 152, 13, 225, 62, 113, 92, 38, 7, 29, 14, 165, 178, 170, 66, 58, 245, 176, 93, 189, 194, 20, 212, 83, 185, 117, 246, 38, 141, 220, 121, 201, 28, 92, 12, 168, 6, 247, 219, 188, 129, 218, 198, 239, 25, 178, 190, 53, 114, 98 }, new byte[] { 63, 25, 40, 1, 88, 199, 173, 217, 245, 54, 59, 135, 200, 176, 222, 121, 142, 117, 184, 71, 95, 223, 13, 140, 162, 1, 183, 202, 62, 155, 245, 183, 167, 229, 227, 220, 29, 238, 217, 66, 181, 75, 195, 156, 39, 4, 226, 180, 90, 121, 216, 121, 167, 187, 130, 250, 83, 245, 86, 171, 115, 14, 117, 238, 38, 28, 238, 250, 193, 118, 43, 23, 149, 95, 184, 30, 70, 146, 21, 166, 189, 102, 64, 111, 96, 182, 185, 213, 26, 215, 159, 134, 157, 96, 81, 81, 171, 128, 16, 118, 194, 6, 182, 69, 232, 237, 236, 28, 31, 229, 56, 3, 127, 38, 214, 223, 17, 234, 247, 6, 80, 197, 24, 167, 202, 100, 198, 118 } });
        }
    }
}
