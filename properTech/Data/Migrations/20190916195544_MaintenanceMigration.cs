using Microsoft.EntityFrameworkCore.Migrations;

namespace properTech.Data.Migrations
{
    public partial class MaintenanceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_maintenanceTeches_Resident_residentId",
                table: "maintenanceTeches");

            migrationBuilder.DropIndex(
                name: "IX_maintenanceTeches_residentId",
                table: "maintenanceTeches");

            migrationBuilder.DropColumn(
                name: "residentId",
                table: "maintenanceTeches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "residentId",
                table: "maintenanceTeches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_maintenanceTeches_residentId",
                table: "maintenanceTeches",
                column: "residentId");

            migrationBuilder.AddForeignKey(
                name: "FK_maintenanceTeches_Resident_residentId",
                table: "maintenanceTeches",
                column: "residentId",
                principalTable: "Resident",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
