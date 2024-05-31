using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayReservation.Migrations.RailwayReservationdb
{
    /// <inheritdoc />
    public partial class dbseatsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Trains",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Trains");
        }
    }
}
