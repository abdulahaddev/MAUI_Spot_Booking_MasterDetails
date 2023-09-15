﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetail_Api.Migrations
{
    public partial class mauidb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    PhoneNo = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Spots",
                columns: table => new
                {
                    SpotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpotName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spots", x => x.SpotId);
                });

            migrationBuilder.CreateTable(
                name: "BookingEntries",
                columns: table => new
                {
                    BookingEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SpotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingEntries", x => x.BookingEntryId);
                    table.ForeignKey(
                        name: "FK_BookingEntries_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingEntries_Spots_SpotId",
                        column: x => x.SpotId,
                        principalTable: "Spots",
                        principalColumn: "SpotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Spots",
                columns: new[] { "SpotId", "SpotName" },
                values: new object[,]
                {
                    { 1, "Panam City" },
                    { 2, "Sylhet" },
                    { 3, "Bandarban" },
                    { 4, "Cox's Bazar" },
                    { 5, "Bagerhat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntries_ClientId",
                table: "BookingEntries",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntries_SpotId",
                table: "BookingEntries",
                column: "SpotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingEntries");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Spots");
        }
    }
}
