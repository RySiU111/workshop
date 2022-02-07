using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class CalendarEntriesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEntry_AspNetUsers_UserId",
                table: "CalendarEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEntry_Subtasks_SubtaskId",
                table: "CalendarEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarEntry",
                table: "CalendarEntry");

            migrationBuilder.RenameTable(
                name: "CalendarEntry",
                newName: "CalendarEntries");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarEntry_UserId",
                table: "CalendarEntries",
                newName: "IX_CalendarEntries_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarEntry_SubtaskId",
                table: "CalendarEntries",
                newName: "IX_CalendarEntries_SubtaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarEntries",
                table: "CalendarEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEntries_AspNetUsers_UserId",
                table: "CalendarEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEntries_Subtasks_SubtaskId",
                table: "CalendarEntries",
                column: "SubtaskId",
                principalTable: "Subtasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEntries_AspNetUsers_UserId",
                table: "CalendarEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEntries_Subtasks_SubtaskId",
                table: "CalendarEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarEntries",
                table: "CalendarEntries");

            migrationBuilder.RenameTable(
                name: "CalendarEntries",
                newName: "CalendarEntry");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarEntries_UserId",
                table: "CalendarEntry",
                newName: "IX_CalendarEntry_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarEntries_SubtaskId",
                table: "CalendarEntry",
                newName: "IX_CalendarEntry_SubtaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarEntry",
                table: "CalendarEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEntry_AspNetUsers_UserId",
                table: "CalendarEntry",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEntry_Subtasks_SubtaskId",
                table: "CalendarEntry",
                column: "SubtaskId",
                principalTable: "Subtasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
