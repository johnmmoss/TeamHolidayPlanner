using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHolidayPlanner.Data.Migrations
{
    public partial class UpdateRoleNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Roles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
