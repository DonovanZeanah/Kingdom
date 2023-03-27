using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingdomWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Cities_CityId",
                table: "Guilds");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Guilds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Cities_CityId",
                table: "Guilds",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Cities_CityId",
                table: "Guilds");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Guilds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Cities_CityId",
                table: "Guilds",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
