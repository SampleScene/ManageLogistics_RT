using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class AddFullTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_stops",
                table: "stops");

            migrationBuilder.RenameTable(
                name: "stops",
                newName: "Stop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stop",
                table: "Stop",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Stop",
                table: "Stop");

            migrationBuilder.RenameTable(
                name: "Stop",
                newName: "stops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stops",
                table: "stops",
                column: "Id");
        }
    }
}
