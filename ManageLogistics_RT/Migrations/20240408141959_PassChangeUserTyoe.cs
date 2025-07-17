using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class PassChangeUserTyoe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStump",
                table: "passReciepts",
                type: "timestump",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "passReciepts",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStump",
                table: "passReciepts",
                type: "timestump",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "passReciepts",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
