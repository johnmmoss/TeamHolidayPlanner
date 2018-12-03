using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHolidayPlanner.Data.Migrations
{
    public partial class RemoveKeyFromPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Permission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Permission",
                nullable: true);
        }
    }
}
