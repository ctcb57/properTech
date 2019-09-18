using Microsoft.EntityFrameworkCore.Migrations;

namespace properTech.Migrations
{
    public partial class anothermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_maintenanceTeches",
                table: "maintenanceTeches");

            migrationBuilder.RenameTable(
                name: "maintenanceTeches",
                newName: "MaintenanceRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceRequest",
                table: "MaintenanceRequest",
                column: "MaintenanceTechId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceRequest",
                table: "MaintenanceRequest");

            migrationBuilder.RenameTable(
                name: "MaintenanceRequest",
                newName: "maintenanceTeches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_maintenanceTeches",
                table: "maintenanceTeches",
                column: "MaintenanceTechId");
        }
    }
}
