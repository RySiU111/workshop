using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.API.Migrations
{
    public partial class InvoiceWorkHoursBruttoNetto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WorkHoursPriceBrutto",
                table: "Invoices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WorkHoursPriceNetto",
                table: "Invoices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkHoursPriceBrutto",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "WorkHoursPriceNetto",
                table: "Invoices");
        }
    }
}
