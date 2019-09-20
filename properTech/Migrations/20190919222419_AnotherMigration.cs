using Microsoft.EntityFrameworkCore.Migrations;

namespace properTech.Migrations
{
    public partial class AnotherMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "confirmationNumber",
                table: "MaintenanceRequest",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmationNumber",
                table: "MaintenanceRequest");
        }
    }
}
