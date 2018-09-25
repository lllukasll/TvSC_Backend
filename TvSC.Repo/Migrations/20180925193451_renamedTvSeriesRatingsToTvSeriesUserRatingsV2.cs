using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class renamedTvSeriesRatingsToTvSeriesUserRatingsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TvSeriesRatings_TvShows_TvShowId",
                table: "TvSeriesRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_TvSeriesRatings_Users_UserId",
                table: "TvSeriesRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TvSeriesRatings",
                table: "TvSeriesRatings");

            migrationBuilder.RenameTable(
                name: "TvSeriesRatings",
                newName: "TvSeriesUserRatings");

            migrationBuilder.RenameIndex(
                name: "IX_TvSeriesRatings_UserId",
                table: "TvSeriesUserRatings",
                newName: "IX_TvSeriesUserRatings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TvSeriesRatings_TvShowId",
                table: "TvSeriesUserRatings",
                newName: "IX_TvSeriesUserRatings_TvShowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TvSeriesUserRatings",
                table: "TvSeriesUserRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TvSeriesUserRatings_TvShows_TvShowId",
                table: "TvSeriesUserRatings",
                column: "TvShowId",
                principalTable: "TvShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TvSeriesUserRatings_Users_UserId",
                table: "TvSeriesUserRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TvSeriesUserRatings_TvShows_TvShowId",
                table: "TvSeriesUserRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_TvSeriesUserRatings_Users_UserId",
                table: "TvSeriesUserRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TvSeriesUserRatings",
                table: "TvSeriesUserRatings");

            migrationBuilder.RenameTable(
                name: "TvSeriesUserRatings",
                newName: "TvSeriesRatings");

            migrationBuilder.RenameIndex(
                name: "IX_TvSeriesUserRatings_UserId",
                table: "TvSeriesRatings",
                newName: "IX_TvSeriesRatings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TvSeriesUserRatings_TvShowId",
                table: "TvSeriesRatings",
                newName: "IX_TvSeriesRatings_TvShowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TvSeriesRatings",
                table: "TvSeriesRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TvSeriesRatings_TvShows_TvShowId",
                table: "TvSeriesRatings",
                column: "TvShowId",
                principalTable: "TvShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TvSeriesRatings_Users_UserId",
                table: "TvSeriesRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
