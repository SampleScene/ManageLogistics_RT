using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class ChangeJsonAddressToAthomar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "stops",
                newName: "Streeyt");

            migrationBuilder.RenameColumn(
                name: "Pasport",
                table: "employees",
                newName: "SerisesAndNumber");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "stops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Home",
                table: "stops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IssuedBy",
                table: "employees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_appUsers_AspNetUsers_UserId",
                table: "appUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appUsers_AspNetUsers_UserId",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "stops");

            migrationBuilder.DropColumn(
                name: "Home",
                table: "stops");

            migrationBuilder.DropColumn(
                name: "IssuedBy",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "Streeyt",
                table: "stops",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "SerisesAndNumber",
                table: "employees",
                newName: "Pasport");
        }
    }
}
