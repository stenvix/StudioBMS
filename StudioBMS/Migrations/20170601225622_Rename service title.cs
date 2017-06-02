using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudioBMS.Migrations
{
    public partial class Renameservicetitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UkName",
                table: "Services",
                newName: "UkTitle");

            migrationBuilder.RenameColumn(
                name: "RuName",
                table: "Services",
                newName: "RuTitle");

            migrationBuilder.RenameColumn(
                name: "EnName",
                table: "Services",
                newName: "EnTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UkTitle",
                table: "Services",
                newName: "UkName");

            migrationBuilder.RenameColumn(
                name: "RuTitle",
                table: "Services",
                newName: "RuName");

            migrationBuilder.RenameColumn(
                name: "EnTitle",
                table: "Services",
                newName: "EnName");
        }
    }
}
