using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class CustomerProcessingPresonalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConsentToTheProcessingOfPersonalData",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentToTheProcessingOfPersonalData",
                table: "Customers");
        }
    }
}
