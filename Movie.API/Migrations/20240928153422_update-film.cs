using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.API.Migrations
{
    /// <inheritdoc />
    public partial class Updatefilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Films",
                newName: "Year");

            migrationBuilder.AddColumn<string>(
                name: "OriginName",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginName",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Films",
                newName: "ReleaseYear");
        }
    }
}
