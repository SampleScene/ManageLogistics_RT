using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IX_passReciepts_PriceId",
                table: "passReciepts",
                column: "PriceId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_passReciepts_prices_PriceId",
                table: "passReciepts",
                column: "PriceId",
                principalTable: "prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_appUsers_AppUserUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_passReciepts_prices_PriceId",
                table: "passReciepts");

            migrationBuilder.DropIndex(
                name: "IX_passReciepts_PriceId",
                table: "passReciepts");

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
    }
}
