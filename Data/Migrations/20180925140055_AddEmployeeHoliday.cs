using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHolidayPlanner.Data.Migrations
{
    public partial class AddEmployeeHoliday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HolidayPeriod",
                columns: table => new
                {
                    HolidayPeriodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPeriod", x => x.HolidayPeriodID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHoliday",
                columns: table => new
                {
                    EmployeeHolidayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    HolidayPeriodID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Authorised = table.Column<bool>(nullable: true),
                    AuthorisedDate = table.Column<DateTime>(nullable: true),
                    AuthorisedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHoliday", x => x.EmployeeHolidayId);
                    table.ForeignKey(
                        name: "FK_EmployeeHoliday_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHoliday_HolidayPeriod_HolidayPeriodID",
                        column: x => x.HolidayPeriodID,
                        principalTable: "HolidayPeriod",
                        principalColumn: "HolidayPeriodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHoliday_EmployeeID",
                table: "EmployeeHoliday",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHoliday_HolidayPeriodID",
                table: "EmployeeHoliday",
                column: "HolidayPeriodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeHoliday");

            migrationBuilder.DropTable(
                name: "HolidayPeriod");
        }
    }
}
