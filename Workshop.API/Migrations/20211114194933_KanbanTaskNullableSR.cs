using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class KanbanTaskNullableSR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_ServiceRequests_ServiceRequestId",
                table: "KanbanTasks");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceRequestId",
                table: "KanbanTasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_ServiceRequests_ServiceRequestId",
                table: "KanbanTasks",
                column: "ServiceRequestId",
                principalTable: "ServiceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_ServiceRequests_ServiceRequestId",
                table: "KanbanTasks");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceRequestId",
                table: "KanbanTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_ServiceRequests_ServiceRequestId",
                table: "KanbanTasks",
                column: "ServiceRequestId",
                principalTable: "ServiceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
