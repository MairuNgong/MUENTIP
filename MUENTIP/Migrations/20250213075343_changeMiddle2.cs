using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUENTIP.Migrations
{
    /// <inheritdoc />
    public partial class changeMiddle2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyOn_AspNetUsers_UserId",
                table: "ApplyOn");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestIn_Tags_TagName",
                table: "InterestIn");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipateIn_Activities_ActivityId",
                table: "ParticipateIn");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "ParticipateIn",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "InterestIn",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ApplyOn",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyOn_AspNetUsers_UserId",
                table: "ApplyOn",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestIn_Tags_TagName",
                table: "InterestIn",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "TagName");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipateIn_Activities_ActivityId",
                table: "ParticipateIn",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyOn_AspNetUsers_UserId",
                table: "ApplyOn");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestIn_Tags_TagName",
                table: "InterestIn");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipateIn_Activities_ActivityId",
                table: "ParticipateIn");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "ParticipateIn",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "InterestIn",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ApplyOn",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyOn_AspNetUsers_UserId",
                table: "ApplyOn",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestIn_Tags_TagName",
                table: "InterestIn",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "TagName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipateIn_Activities_ActivityId",
                table: "ParticipateIn",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
