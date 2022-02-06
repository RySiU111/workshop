using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class UserKanbanTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "KanbanTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanbanTasks_UserId",
                table: "KanbanTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_AspNetUsers_UserId",
                table: "KanbanTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_AspNetUsers_UserId",
                table: "KanbanTasks");

            migrationBuilder.DropIndex(
                name: "IX_KanbanTasks_UserId",
                table: "KanbanTasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "KanbanTasks");
        }
    }
}
