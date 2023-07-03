using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RentalStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 3, 11, 36, 45, 924, DateTimeKind.Utc).AddTicks(1625), new byte[] { 215, 188, 196, 246, 83, 218, 17, 139, 210, 209, 110, 175, 157, 202, 33, 178, 121, 134, 121, 45, 220, 213, 154, 94, 43, 136, 235, 140, 189, 112, 114, 204, 1, 255, 123, 96, 222, 97, 157, 165, 155, 241, 190, 65, 149, 152, 40, 51, 101, 11, 184, 188, 69, 236, 10, 241, 120, 4, 153, 31, 171, 229, 3, 190 }, new byte[] { 115, 0, 83, 127, 236, 128, 57, 64, 105, 71, 196, 156, 225, 66, 145, 21, 62, 244, 133, 18, 1, 186, 231, 111, 139, 79, 30, 0, 30, 192, 249, 130, 157, 9, 206, 113, 83, 67, 222, 116, 20, 147, 21, 148, 209, 246, 41, 42, 86, 254, 135, 180, 168, 18, 227, 112, 201, 27, 84, 251, 138, 255, 67, 119, 138, 44, 87, 159, 113, 0, 151, 62, 194, 224, 103, 173, 207, 149, 129, 141, 23, 21, 190, 170, 30, 1, 125, 145, 96, 44, 231, 195, 19, 33, 38, 234, 41, 164, 107, 226, 161, 58, 59, 61, 128, 69, 48, 212, 49, 246, 76, 66, 42, 251, 2, 156, 152, 212, 82, 15, 97, 5, 45, 30, 60, 24, 111, 141 } });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 6, 23, 11, 28, 6, 520, DateTimeKind.Utc).AddTicks(7757), new byte[] { 224, 14, 236, 84, 111, 244, 94, 10, 182, 55, 94, 221, 215, 132, 243, 39, 197, 126, 133, 37, 58, 27, 22, 92, 97, 206, 57, 242, 119, 2, 25, 141, 213, 66, 255, 106, 57, 147, 128, 134, 229, 223, 107, 41, 117, 116, 176, 141, 166, 18, 24, 35, 232, 148, 15, 155, 213, 47, 141, 97, 43, 69, 152, 143 }, new byte[] { 48, 242, 234, 5, 251, 9, 178, 72, 128, 88, 224, 198, 240, 43, 22, 144, 49, 82, 61, 125, 118, 126, 89, 12, 234, 242, 115, 90, 1, 47, 251, 247, 113, 23, 183, 154, 210, 207, 214, 4, 63, 63, 173, 85, 20, 85, 250, 90, 160, 72, 156, 135, 74, 9, 115, 158, 195, 84, 130, 40, 2, 27, 177, 235, 21, 70, 194, 72, 248, 188, 134, 189, 145, 183, 134, 167, 33, 195, 44, 20, 93, 147, 168, 93, 22, 58, 115, 94, 90, 73, 81, 178, 124, 10, 32, 101, 78, 196, 91, 218, 119, 0, 194, 220, 55, 37, 29, 207, 37, 78, 11, 60, 245, 242, 221, 40, 137, 85, 18, 106, 254, 133, 244, 138, 251, 103, 243, 117 } });
        }
    }
}
