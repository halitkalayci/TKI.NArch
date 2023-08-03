﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 8, 2, 10, 56, 16, 905, DateTimeKind.Utc).AddTicks(6247), new byte[] { 212, 182, 17, 16, 172, 189, 62, 24, 83, 1, 37, 66, 173, 239, 238, 4, 233, 55, 133, 69, 236, 67, 13, 81, 58, 204, 158, 86, 144, 8, 152, 121, 235, 116, 93, 104, 8, 19, 186, 105, 127, 233, 205, 81, 72, 215, 206, 105, 53, 230, 196, 60, 204, 129, 199, 172, 52, 227, 45, 78, 15, 230, 212, 178 }, new byte[] { 186, 72, 67, 161, 1, 146, 19, 89, 221, 239, 42, 215, 116, 163, 55, 189, 69, 17, 189, 207, 118, 210, 169, 220, 46, 182, 142, 87, 88, 253, 27, 46, 42, 208, 202, 48, 171, 165, 48, 225, 165, 124, 223, 181, 158, 227, 166, 52, 234, 178, 121, 247, 126, 139, 138, 181, 120, 108, 122, 248, 174, 108, 202, 121, 131, 108, 148, 213, 232, 20, 77, 188, 203, 146, 89, 241, 173, 241, 233, 129, 170, 157, 107, 253, 196, 151, 68, 111, 24, 31, 51, 163, 53, 173, 254, 10, 225, 61, 251, 224, 174, 223, 31, 165, 184, 34, 208, 195, 40, 95, 14, 23, 168, 226, 185, 223, 36, 141, 233, 114, 252, 69, 179, 96, 44, 233, 45, 34 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 21, 11, 41, 54, 81, DateTimeKind.Utc).AddTicks(2327), new byte[] { 7, 231, 21, 221, 95, 192, 136, 206, 127, 139, 211, 59, 76, 251, 181, 150, 158, 197, 240, 111, 118, 94, 173, 236, 134, 253, 9, 102, 20, 231, 6, 113, 47, 221, 117, 111, 133, 218, 13, 198, 73, 41, 182, 136, 19, 230, 189, 88, 250, 180, 205, 7, 24, 252, 39, 71, 251, 29, 107, 67, 92, 62, 50, 8 }, new byte[] { 185, 129, 8, 199, 89, 9, 114, 231, 95, 128, 156, 210, 142, 230, 184, 186, 87, 189, 110, 141, 72, 75, 66, 196, 175, 131, 0, 153, 13, 74, 110, 143, 9, 160, 49, 141, 243, 249, 92, 48, 63, 234, 239, 219, 33, 158, 146, 220, 219, 19, 100, 99, 204, 179, 237, 153, 55, 87, 182, 50, 198, 200, 197, 221, 254, 132, 84, 15, 54, 66, 90, 47, 255, 117, 252, 176, 173, 214, 67, 230, 199, 65, 147, 167, 207, 22, 79, 146, 93, 151, 214, 60, 32, 92, 20, 13, 100, 117, 125, 76, 124, 212, 246, 209, 42, 35, 37, 236, 242, 9, 120, 211, 193, 24, 210, 209, 17, 183, 247, 18, 166, 34, 234, 43, 240, 44, 19, 83 } });
        }
    }
}