using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileUploads_Users_UserId",
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
                values: new object[] { new DateTime(2023, 8, 2, 11, 1, 34, 999, DateTimeKind.Utc).AddTicks(3242), new byte[] { 111, 21, 177, 245, 28, 228, 244, 71, 158, 219, 108, 126, 109, 138, 38, 152, 13, 225, 62, 113, 92, 38, 7, 29, 14, 165, 178, 170, 66, 58, 245, 176, 93, 189, 194, 20, 212, 83, 185, 117, 246, 38, 141, 220, 121, 201, 28, 92, 12, 168, 6, 247, 219, 188, 129, 218, 198, 239, 25, 178, 190, 53, 114, 98 }, new byte[] { 63, 25, 40, 1, 88, 199, 173, 217, 245, 54, 59, 135, 200, 176, 222, 121, 142, 117, 184, 71, 95, 223, 13, 140, 162, 1, 183, 202, 62, 155, 245, 183, 167, 229, 227, 220, 29, 238, 217, 66, 181, 75, 195, 156, 39, 4, 226, 180, 90, 121, 216, 121, 167, 187, 130, 250, 83, 245, 86, 171, 115, 14, 117, 238, 38, 28, 238, 250, 193, 118, 43, 23, 149, 95, 184, 30, 70, 146, 21, 166, 189, 102, 64, 111, 96, 182, 185, 213, 26, 215, 159, 134, 157, 96, 81, 81, 171, 128, 16, 118, 194, 6, 182, 69, 232, 237, 236, 28, 31, 229, 56, 3, 127, 38, 214, 223, 17, 234, 247, 6, 80, 197, 24, 167, 202, 100, 198, 118 } });

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_UserId",
                table: "FileUploads",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 8, 2, 10, 56, 16, 905, DateTimeKind.Utc).AddTicks(6247), new byte[] { 212, 182, 17, 16, 172, 189, 62, 24, 83, 1, 37, 66, 173, 239, 238, 4, 233, 55, 133, 69, 236, 67, 13, 81, 58, 204, 158, 86, 144, 8, 152, 121, 235, 116, 93, 104, 8, 19, 186, 105, 127, 233, 205, 81, 72, 215, 206, 105, 53, 230, 196, 60, 204, 129, 199, 172, 52, 227, 45, 78, 15, 230, 212, 178 }, new byte[] { 186, 72, 67, 161, 1, 146, 19, 89, 221, 239, 42, 215, 116, 163, 55, 189, 69, 17, 189, 207, 118, 210, 169, 220, 46, 182, 142, 87, 88, 253, 27, 46, 42, 208, 202, 48, 171, 165, 48, 225, 165, 124, 223, 181, 158, 227, 166, 52, 234, 178, 121, 247, 126, 139, 138, 181, 120, 108, 122, 248, 174, 108, 202, 121, 131, 108, 148, 213, 232, 20, 77, 188, 203, 146, 89, 241, 173, 241, 233, 129, 170, 157, 107, 253, 196, 151, 68, 111, 24, 31, 51, 163, 53, 173, 254, 10, 225, 61, 251, 224, 174, 223, 31, 165, 184, 34, 208, 195, 40, 95, 14, 23, 168, 226, 185, 223, 36, 141, 233, 114, 252, 69, 179, 96, 44, 233, 45, 34 } });
        }
    }
}
