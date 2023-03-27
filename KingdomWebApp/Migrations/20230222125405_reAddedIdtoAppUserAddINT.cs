using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingdomWebApp.Migrations
{
    public partial class reAddedIdtoAppUserAddINT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Users_AppUserId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_AppUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewers_Users_AppUserId",
                table: "Reviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_AppUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_AppUserId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Users_AppUserId",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Users_AppUserId",
                table: "Clubs",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_AppUserId",
                table: "Projects",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewers_Users_AppUserId",
                table: "Reviewers",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_AppUserId",
                table: "Reviews",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_AppUserId",
                table: "Skills",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Users_AppUserId",
                table: "Tools",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Users_AppUserId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_AppUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewers_Users_AppUserId",
                table: "Reviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_AppUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_AppUserId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Users_AppUserId",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Users_AppUserId",
                table: "Clubs",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_AppUserId",
                table: "Projects",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewers_Users_AppUserId",
                table: "Reviewers",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_AppUserId",
                table: "Reviews",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_AppUserId",
                table: "Skills",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Users_AppUserId",
                table: "Tools",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
