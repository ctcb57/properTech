using Microsoft.EntityFrameworkCore.Migrations;

namespace properTech.Migrations
{
    public partial class residentsmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Resident",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Resident",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAssignedUnit",
                table: "Resident",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Resident");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Resident");

            migrationBuilder.DropColumn(
                name: "isAssignedUnit",
                table: "Resident");
        }
    }
}
