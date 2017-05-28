using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudioBMS.Migrations
{
    public partial class Workshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timetables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTimeTables",
                columns: table => new
                {
                    TimeTableId = table.Column<Guid>(nullable: false),
                    WorkshopId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTimeTables", x => new { x.TimeTableId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_ItemTimeTables_Timetables_TimeTableId",
                        column: x => x.TimeTableId,
                        principalTable: "Timetables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTimeTables_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemTimeTables_WorkshopId",
                table: "ItemTimeTables",
                column: "WorkshopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTimeTables");

            migrationBuilder.DropTable(
                name: "Timetables");

            migrationBuilder.DropTable(
                name: "Workshops");
        }
    }
}
