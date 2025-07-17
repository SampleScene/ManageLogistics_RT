using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class DropUserAndEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_appUsers_AppUserUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserUserId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserUserId",
                table: "AspNetUsers",
                column: "AppUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_appUsers_AppUserUserId",
                table: "AspNetUsers",
                column: "AppUserUserId",
                principalTable: "appUsers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "EmployeeId");
        }
    }
}
