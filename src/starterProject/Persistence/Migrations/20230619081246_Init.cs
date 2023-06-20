using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table =>
                    new
                    {
                        Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        Kilometer = table.Column<int>(type: "int", nullable: false),
                        Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        MinFindeksCreditRate = table.Column<short>(type: "smallint", nullable: false),
                        State = table.Column<int>(type: "int", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                        DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Cars");
        }
    }
}
