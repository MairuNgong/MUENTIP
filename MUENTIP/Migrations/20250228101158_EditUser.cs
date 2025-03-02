using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUENTIP.Migrations
{
    /// <inheritdoc />
    public partial class EditUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTypes_Tags_TagName",
                table: "ActivityTypes");

            migrationBuilder.AddColumn<bool>(
                name: "ShowCreate",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowParticipate",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "ActivityTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTypes_Tags_TagName",
                table: "ActivityTypes",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "TagName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTypes_Tags_TagName",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "ShowCreate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShowParticipate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "ActivityTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTypes_Tags_TagName",
                table: "ActivityTypes",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "TagName");
        }
    }
}
