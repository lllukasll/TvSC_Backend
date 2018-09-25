using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class UpdatedTvSeriesRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TvSeriesRatings_Users_UserId1",
                table: "TvSeriesRatings");

            migrationBuilder.DropIndex(
                name: "IX_TvSeriesRatings_UserId1",
                table: "TvSeriesRatings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TvSeriesRatings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TvSeriesRatings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TvSeriesRatings_UserId",
                table: "TvSeriesRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TvSeriesRatings_Users_UserId",
                table: "TvSeriesRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TvSeriesRatings_Users_UserId",
                table: "TvSeriesRatings");

            migrationBuilder.DropIndex(
                name: "IX_TvSeriesRatings_UserId",
                table: "TvSeriesRatings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TvSeriesRatings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TvSeriesRatings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TvSeriesRatings_UserId1",
                table: "TvSeriesRatings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TvSeriesRatings_Users_UserId1",
                table: "TvSeriesRatings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
