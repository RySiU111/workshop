using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infra.Migrations
{
    public partial class ComponentConfigModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentConfigs",
                table: "ComponentConfigs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ComponentConfigs");

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "ComponentConfigs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComponentName",
                table: "ComponentConfigs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentConfigs",
                table: "ComponentConfigs",
                column: "ComponentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentConfigs",
                table: "ComponentConfigs");

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "ComponentConfigs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ComponentName",
                table: "ComponentConfigs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ComponentConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentConfigs",
                table: "ComponentConfigs",
                column: "Id");
        }
    }
}
