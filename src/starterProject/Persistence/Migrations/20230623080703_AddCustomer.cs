using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CustomerNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
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
                values: new object[] { new DateTime(2023, 6, 23, 8, 7, 3, 594, DateTimeKind.Utc).AddTicks(9920), new byte[] { 101, 168, 103, 46, 17, 151, 252, 104, 112, 93, 214, 191, 10, 148, 223, 227, 156, 6, 202, 166, 170, 138, 125, 131, 204, 75, 143, 251, 111, 4, 12, 63, 157, 54, 250, 183, 154, 107, 33, 96, 3, 145, 98, 71, 118, 120, 135, 96, 89, 53, 208, 181, 247, 101, 142, 119, 12, 176, 161, 205, 239, 91, 83, 152 }, new byte[] { 220, 33, 140, 209, 31, 8, 208, 192, 106, 148, 15, 205, 209, 127, 251, 253, 71, 80, 14, 47, 11, 34, 107, 39, 168, 213, 104, 100, 240, 196, 81, 175, 66, 3, 83, 86, 220, 164, 180, 150, 248, 95, 177, 75, 182, 0, 221, 246, 101, 138, 126, 210, 108, 114, 88, 141, 113, 78, 84, 68, 180, 102, 107, 195, 218, 148, 197, 77, 146, 89, 40, 246, 232, 8, 179, 236, 133, 196, 92, 231, 181, 11, 194, 86, 54, 184, 163, 158, 5, 129, 150, 124, 154, 212, 70, 226, 194, 64, 33, 84, 64, 105, 213, 4, 66, 111, 50, 68, 135, 53, 151, 141, 148, 139, 87, 245, 127, 144, 216, 196, 101, 217, 122, 67, 27, 100, 55, 188 } });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 6, 21, 8, 29, 13, 112, DateTimeKind.Utc).AddTicks(7064), new byte[] { 254, 203, 122, 53, 134, 3, 244, 210, 81, 176, 94, 218, 200, 245, 162, 29, 224, 213, 128, 249, 210, 180, 216, 68, 207, 44, 24, 243, 180, 128, 253, 24, 30, 108, 25, 196, 19, 234, 169, 99, 112, 89, 181, 2, 240, 13, 95, 174, 46, 158, 230, 20, 146, 133, 123, 106, 229, 153, 106, 167, 188, 158, 175, 112 }, new byte[] { 160, 66, 211, 26, 227, 86, 182, 166, 19, 140, 21, 98, 125, 189, 160, 47, 0, 229, 107, 211, 32, 171, 83, 72, 99, 108, 168, 242, 121, 108, 62, 40, 89, 248, 44, 80, 115, 226, 249, 95, 235, 38, 36, 91, 32, 159, 232, 66, 196, 85, 19, 2, 218, 33, 198, 230, 55, 144, 200, 52, 46, 246, 149, 116, 101, 110, 163, 246, 224, 31, 142, 137, 225, 227, 28, 141, 145, 145, 251, 43, 82, 82, 50, 217, 145, 205, 18, 101, 64, 215, 196, 98, 106, 250, 149, 152, 183, 119, 240, 200, 62, 86, 231, 25, 186, 133, 10, 236, 29, 76, 239, 234, 94, 33, 141, 27, 73, 35, 155, 2, 10, 148, 222, 213, 241, 251, 200, 57 } });
        }
    }
}
