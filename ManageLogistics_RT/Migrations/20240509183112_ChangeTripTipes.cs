using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTripTipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStart",
                table: "Trips",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeEnd",
                table: "Trips",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeStart",
                table: "Trips",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeEnd",
                table: "Trips",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RouteId",
                table: "Trips",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
