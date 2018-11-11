using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class updatedActorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "TvShows",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_ActorId",
                table: "TvShows",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TvShows_Actors_ActorId",
                table: "TvShows",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TvShows_Actors_ActorId",
                table: "TvShows");

            migrationBuilder.DropIndex(
                name: "IX_TvShows_ActorId",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "TvShows");
        }
    }
}
