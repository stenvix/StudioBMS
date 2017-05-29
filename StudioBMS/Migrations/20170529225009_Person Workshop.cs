using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudioBMS.Migrations
{
    public partial class PersonWorkshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTimetables_Timetables_TimeTableId",
                table: "PersonTimetables");

            migrationBuilder.RenameColumn(
                name: "TimeTableId",
                table: "PersonTimetables",
                newName: "TimetableId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTimetables_TimeTableId",
                table: "PersonTimetables",
                newName: "IX_PersonTimetables_TimetableId");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkshopId",
                table: "Persons",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Persons_WorkshopId",
                table: "Persons",
                column: "WorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Workshops_WorkshopId",
                table: "Persons",
                column: "WorkshopId",
                principalTable: "Workshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTimetables_Timetables_TimetableId",
                table: "PersonTimetables",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Workshops_WorkshopId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonTimetables_Timetables_TimetableId",
                table: "PersonTimetables");

            migrationBuilder.DropIndex(
                name: "IX_Persons_WorkshopId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "WorkshopId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "TimetableId",
                table: "PersonTimetables",
                newName: "TimeTableId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTimetables_TimetableId",
                table: "PersonTimetables",
                newName: "IX_PersonTimetables_TimeTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTimetables_Timetables_TimeTableId",
                table: "PersonTimetables",
                column: "TimeTableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
