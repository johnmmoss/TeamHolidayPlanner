﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHolidayPlanner.Data.Migrations
{
    public partial class HolidayPeriodModificationDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "HolidayPeriod",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "HolidayPeriod",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "HolidayPeriod");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "HolidayPeriod");
        }
    }
}
