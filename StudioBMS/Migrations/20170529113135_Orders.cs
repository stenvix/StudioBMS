using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudioBMS.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonService_Persons_PersonId",
                table: "PersonService");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonService_Services_ServiceId",
                table: "PersonService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonService",
                table: "PersonService");

            migrationBuilder.RenameTable(
                name: "PersonService",
                newName: "PersonServices");

            migrationBuilder.RenameIndex(
                name: "IX_PersonService_PersonId",
                table: "PersonServices",
                newName: "IX_PersonServices_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonServices",
                table: "PersonServices",
                columns: new[] { "ServiceId", "PersonId" });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: false),
                    PerformerId = table.Column<Guid>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    WorkshopId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderServices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PerformerId",
                table: "Orders",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WorkshopId",
                table: "Orders",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_OrderId",
                table: "OrderServices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_ServiceId",
                table: "OrderServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonServices_Persons_PersonId",
                table: "PersonServices",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonServices_Services_ServiceId",
                table: "PersonServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonServices_Persons_PersonId",
                table: "PersonServices");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonServices_Services_ServiceId",
                table: "PersonServices");

            migrationBuilder.DropTable(
                name: "OrderServices");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonServices",
                table: "PersonServices");

            migrationBuilder.RenameTable(
                name: "PersonServices",
                newName: "PersonService");

            migrationBuilder.RenameIndex(
                name: "IX_PersonServices_PersonId",
                table: "PersonService",
                newName: "IX_PersonService_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonService",
                table: "PersonService",
                columns: new[] { "ServiceId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonService_Persons_PersonId",
                table: "PersonService",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonService_Services_ServiceId",
                table: "PersonService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
