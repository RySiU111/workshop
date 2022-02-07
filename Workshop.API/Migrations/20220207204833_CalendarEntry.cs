using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Workshop.API.Migrations
{
    public partial class CalendarEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SubtaskId = table.Column<int>(type: "integer", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Hours = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarEntry_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarEntry_Subtasks_SubtaskId",
                        column: x => x.SubtaskId,
                        principalTable: "Subtasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntry_SubtaskId",
                table: "CalendarEntry",
                column: "SubtaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntry_UserId",
                table: "CalendarEntry",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEntry");
        }
    }
}
