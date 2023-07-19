using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTreeContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTreeContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowOrder = table.Column<int>(type: "int", nullable: false),
                    ShowOnAuth = table.Column<bool>(type: "bit", nullable: false),
                    HideOnAuth = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTreeContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupTreeContentOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupTreeContentId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTreeContentOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTreeContentOperationClaims_GroupTreeContents_GroupTreeContentId",
                        column: x => x.GroupTreeContentId,
                        principalTable: "GroupTreeContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTreeContentOperationClaims_OperationClaim_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 19, 10, 55, 12, 228, DateTimeKind.Utc).AddTicks(8447), new byte[] { 147, 215, 74, 216, 137, 206, 230, 182, 199, 106, 51, 188, 146, 111, 101, 159, 145, 239, 131, 148, 171, 160, 193, 118, 225, 14, 114, 153, 212, 87, 98, 226, 75, 229, 38, 226, 76, 246, 98, 49, 27, 252, 97, 250, 201, 188, 167, 68, 29, 100, 13, 146, 78, 157, 137, 48, 80, 54, 182, 106, 19, 199, 127, 183 }, new byte[] { 109, 68, 130, 124, 107, 161, 127, 36, 222, 0, 180, 118, 137, 108, 197, 195, 1, 77, 95, 251, 130, 195, 19, 252, 8, 69, 69, 229, 92, 132, 254, 180, 70, 81, 180, 1, 160, 94, 172, 83, 240, 239, 127, 175, 122, 38, 126, 72, 96, 144, 73, 225, 249, 110, 158, 64, 123, 153, 223, 4, 80, 79, 46, 179, 97, 125, 37, 238, 173, 174, 33, 129, 217, 6, 204, 27, 214, 28, 119, 17, 224, 227, 178, 146, 116, 14, 63, 94, 185, 169, 236, 31, 64, 203, 97, 173, 9, 190, 224, 144, 171, 111, 176, 195, 146, 224, 50, 52, 20, 221, 234, 131, 57, 118, 7, 11, 97, 42, 128, 76, 202, 28, 171, 84, 153, 19, 66, 110 } });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTreeContentOperationClaims_GroupTreeContentId",
                table: "GroupTreeContentOperationClaims",
                column: "GroupTreeContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTreeContentOperationClaims_OperationClaimId",
                table: "GroupTreeContentOperationClaims",
                column: "OperationClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTreeContentOperationClaims");

            migrationBuilder.DropTable(
                name: "GroupTreeContents");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 7, 11, 6, 57, 44, 991, DateTimeKind.Utc).AddTicks(9068), new byte[] { 184, 212, 244, 60, 111, 88, 209, 225, 179, 78, 9, 21, 153, 105, 241, 172, 45, 129, 27, 16, 122, 225, 157, 192, 159, 215, 230, 103, 161, 103, 240, 158, 251, 86, 216, 8, 57, 101, 219, 148, 59, 170, 150, 16, 40, 75, 125, 69, 113, 69, 62, 15, 226, 0, 242, 73, 20, 91, 241, 235, 179, 204, 102, 166 }, new byte[] { 51, 160, 145, 78, 135, 201, 222, 144, 253, 154, 235, 59, 225, 28, 128, 112, 171, 42, 220, 169, 115, 49, 187, 156, 149, 207, 51, 250, 219, 5, 181, 112, 182, 245, 152, 81, 65, 231, 249, 212, 44, 68, 234, 164, 36, 130, 235, 9, 35, 66, 19, 220, 215, 204, 147, 244, 151, 22, 253, 235, 121, 47, 181, 121, 22, 127, 189, 28, 230, 86, 29, 229, 133, 129, 42, 62, 131, 143, 27, 209, 16, 144, 179, 86, 114, 159, 236, 173, 193, 253, 62, 170, 236, 96, 81, 228, 240, 167, 220, 163, 110, 159, 230, 217, 246, 168, 201, 59, 106, 11, 141, 103, 226, 59, 9, 101, 18, 48, 171, 212, 20, 229, 55, 41, 246, 62, 67, 250 } });
        }
    }
}
