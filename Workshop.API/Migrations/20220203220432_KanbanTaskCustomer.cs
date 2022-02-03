using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class KanbanTaskCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "KanbanTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanbanTasks_CustomerId",
                table: "KanbanTasks",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_Customers_CustomerId",
                table: "KanbanTasks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_Customers_CustomerId",
                table: "KanbanTasks");

            migrationBuilder.DropIndex(
                name: "IX_KanbanTasks_CustomerId",
                table: "KanbanTasks");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "KanbanTasks");
        }
    }
}
