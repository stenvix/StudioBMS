using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudioBMS.Migrations
{
    public partial class Services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTimeTables");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Persons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Persons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PersonTimetables",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    TimeTableId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTimetables", x => new { x.PersonId, x.TimeTableId });
                    table.ForeignKey(
                        name: "FK_PersonTimetables_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonTimetables_Timetables_TimeTableId",
                        column: x => x.TimeTableId,
                        principalTable: "Timetables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Duration = table.Column<DateTime>(nullable: false),
                    EnName = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    RuName = table.Column<string>(nullable: false),
                    UkName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopTimetables",
                columns: table => new
                {
                    TimetableId = table.Column<Guid>(nullable: false),
                    WorkshopId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopTimetables", x => new { x.TimetableId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_WorkshopTimetables_Timetables_TimetableId",
                        column: x => x.TimetableId,
                        principalTable: "Timetables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopTimetables_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonService",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonService", x => new { x.ServiceId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonService_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonService_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonService_PersonId",
                table: "PersonService",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTimetables_TimeTableId",
                table: "PersonTimetables",
                column: "TimeTableId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopTimetables_WorkshopId",
                table: "WorkshopTimetables",
                column: "WorkshopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonService");

            migrationBuilder.DropTable(
                name: "PersonTimetables");

            migrationBuilder.DropTable(
                name: "WorkshopTimetables");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Persons");

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
    }
}
