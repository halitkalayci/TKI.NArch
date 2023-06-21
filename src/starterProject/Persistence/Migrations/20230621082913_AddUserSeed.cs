using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status", "UpdatedDate" },
                values: new object[] { 1, 0, new DateTime(2023, 6, 21, 8, 29, 13, 112, DateTimeKind.Utc).AddTicks(7064), null, "tki@tki.com", "TKI", "TKI", new byte[] { 254, 203, 122, 53, 134, 3, 244, 210, 81, 176, 94, 218, 200, 245, 162, 29, 224, 213, 128, 249, 210, 180, 216, 68, 207, 44, 24, 243, 180, 128, 253, 24, 30, 108, 25, 196, 19, 234, 169, 99, 112, 89, 181, 2, 240, 13, 95, 174, 46, 158, 230, 20, 146, 133, 123, 106, 229, 153, 106, 167, 188, 158, 175, 112 }, new byte[] { 160, 66, 211, 26, 227, 86, 182, 166, 19, 140, 21, 98, 125, 189, 160, 47, 0, 229, 107, 211, 32, 171, 83, 72, 99, 108, 168, 242, 121, 108, 62, 40, 89, 248, 44, 80, 115, 226, 249, 95, 235, 38, 36, 91, 32, 159, 232, 66, 196, 85, 19, 2, 218, 33, 198, 230, 55, 144, 200, 52, 46, 246, 149, 116, 101, 110, 163, 246, 224, 31, 142, 137, 225, 227, 28, 141, 145, 145, 251, 43, 82, 82, 50, 217, 145, 205, 18, 101, 64, 215, 196, 98, 106, 250, 149, 152, 183, 119, 240, 200, 62, 86, 231, 25, 186, 133, 10, 236, 29, 76, 239, 234, 94, 33, 141, 27, 73, 35, 155, 2, 10, 148, 222, 213, 241, 251, 200, 57 }, true, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
