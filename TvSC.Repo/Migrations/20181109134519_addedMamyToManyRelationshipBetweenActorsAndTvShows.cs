using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class addedMamyToManyRelationshipBetweenActorsAndTvShows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_TvShows_TvShowId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_TvShows_Actors_ActorId",
                table: "TvShows");

            migrationBuilder.DropIndex(
                name: "IX_TvShows_ActorId",
                table: "TvShows");

            migrationBuilder.DropIndex(
                name: "IX_Actors_TvShowId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "TvShowId",
                table: "Actors");

            migrationBuilder.CreateTable(
                name: "ActorsAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActorId = table.Column<int>(nullable: true),
                    TvShowId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorsAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorsAssignments_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActorsAssignments_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorsAssignments_ActorId",
                table: "ActorsAssignments",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorsAssignments_TvShowId",
                table: "ActorsAssignments",
                column: "TvShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorsAssignments");

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "TvShows",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TvShowId",
                table: "Actors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_ActorId",
                table: "TvShows",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_TvShowId",
                table: "Actors",
                column: "TvShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_TvShows_TvShowId",
                table: "Actors",
                column: "TvShowId",
                principalTable: "TvShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TvShows_Actors_ActorId",
                table: "TvShows",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
