using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudioBMS.Migrations
{
    public partial class Workshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Timetables",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    End = table.Column<DateTime>("datetime2", nullable: false),
                    Start = table.Column<DateTime>("datetime2", nullable: false),
                    WeekDay = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Timetables", x => x.Id); });

            migrationBuilder.CreateTable(
                "Workshops",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Workshops", x => x.Id); });

            migrationBuilder.CreateTable(
                "ItemTimeTables",
                table => new
                {
                    TimeTableId = table.Column<Guid>(nullable: false),
                    WorkshopId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTimeTables", x => new {x.TimeTableId, x.WorkshopId});
                    table.ForeignKey(
                        "FK_ItemTimeTables_Timetables_TimeTableId",
                        x => x.TimeTableId,
                        "Timetables",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ItemTimeTables_Workshops_WorkshopId",
                        x => x.WorkshopId,
                        "Workshops",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_ItemTimeTables_WorkshopId",
                "ItemTimeTables",
                "WorkshopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ItemTimeTables");

            migrationBuilder.DropTable(
                "Timetables");

            migrationBuilder.DropTable(
                "Workshops");
        }
    }
}