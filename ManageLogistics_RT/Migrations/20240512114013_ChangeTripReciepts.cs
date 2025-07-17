using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageLogistics_RT.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTripReciepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "TripReceipts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripId",
                table: "TripReceipts");
        }
    }
}
