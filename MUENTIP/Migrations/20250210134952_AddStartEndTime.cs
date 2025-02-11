using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUENTIP.Migrations
{
    /// <inheritdoc />
    public partial class AddStartEndTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityDateTime",
                table: "Activities",
                newName: "StartDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "Activities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Activities",
                newName: "ActivityDateTime");
        }
    }
}
