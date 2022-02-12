using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class CalendarEntryModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "CalendarEntries",
                newName: "DateOfCreation");

            migrationBuilder.RenameColumn(
                name: "DateFrom",
                table: "CalendarEntries",
                newName: "Date");

            migrationBuilder.AddColumn<bool>(
                name: "IsPlanned",
                table: "CalendarEntries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlanned",
                table: "CalendarEntries");

            migrationBuilder.RenameColumn(
                name: "DateOfCreation",
                table: "CalendarEntries",
                newName: "DateTo");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "CalendarEntries",
                newName: "DateFrom");
        }
    }
}
