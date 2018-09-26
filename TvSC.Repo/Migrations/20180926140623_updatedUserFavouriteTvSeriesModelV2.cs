using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class updatedUserFavouriteTvSeriesModelV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavouriteTvShows_Users_UserId1",
                table: "UserFavouriteTvShows");

            migrationBuilder.DropIndex(
                name: "IX_UserFavouriteTvShows_UserId1",
                table: "UserFavouriteTvShows");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserFavouriteTvShows");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFavouriteTvShows",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteTvShows_UserId",
                table: "UserFavouriteTvShows",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavouriteTvShows_Users_UserId",
                table: "UserFavouriteTvShows",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavouriteTvShows_Users_UserId",
                table: "UserFavouriteTvShows");

            migrationBuilder.DropIndex(
                name: "IX_UserFavouriteTvShows_UserId",
                table: "UserFavouriteTvShows");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserFavouriteTvShows",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserFavouriteTvShows",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteTvShows_UserId1",
                table: "UserFavouriteTvShows",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavouriteTvShows_Users_UserId1",
                table: "UserFavouriteTvShows",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
