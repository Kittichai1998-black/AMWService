using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMWService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ams_ServiceOrder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    SO = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustommerID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    PriorityID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    RootcauseID = table.Column<int>(type: "int", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ams_ServiceOrder", x => x.SO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ams_ServiceOrder");
        }
    }
}
