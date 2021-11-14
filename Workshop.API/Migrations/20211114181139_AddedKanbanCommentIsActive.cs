using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class AddedKanbanCommentIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "KanbanComments",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "KanbanComments");
        }
    }
}
