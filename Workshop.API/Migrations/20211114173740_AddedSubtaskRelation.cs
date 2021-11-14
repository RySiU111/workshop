using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class AddedSubtaskRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtask_KanbanTasks_KanbanTaskId",
                table: "Subtask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtask",
                table: "Subtask");

            migrationBuilder.RenameTable(
                name: "Subtask",
                newName: "Subtasks");

            migrationBuilder.RenameIndex(
                name: "IX_Subtask_KanbanTaskId",
                table: "Subtasks",
                newName: "IX_Subtasks_KanbanTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtasks",
                table: "Subtasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtasks_KanbanTasks_KanbanTaskId",
                table: "Subtasks",
                column: "KanbanTaskId",
                principalTable: "KanbanTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtasks_KanbanTasks_KanbanTaskId",
                table: "Subtasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtasks",
                table: "Subtasks");

            migrationBuilder.RenameTable(
                name: "Subtasks",
                newName: "Subtask");

            migrationBuilder.RenameIndex(
                name: "IX_Subtasks_KanbanTaskId",
                table: "Subtask",
                newName: "IX_Subtask_KanbanTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtask",
                table: "Subtask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtask_KanbanTasks_KanbanTaskId",
                table: "Subtask",
                column: "KanbanTaskId",
                principalTable: "KanbanTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
