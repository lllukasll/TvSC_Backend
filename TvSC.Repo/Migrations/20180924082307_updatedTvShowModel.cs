using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class updatedTvShowModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TvShows");

            migrationBuilder.RenameColumn(
                name: "Hour",
                table: "TvShows",
                newName: "Network");

            migrationBuilder.RenameColumn(
                name: "Episode",
                table: "TvShows",
                newName: "EmissionHour");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TvShows",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeLength",
                table: "TvShows",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "EpisodeLength",
                table: "TvShows");

            migrationBuilder.RenameColumn(
                name: "Network",
                table: "TvShows",
                newName: "Hour");

            migrationBuilder.RenameColumn(
                name: "EmissionHour",
                table: "TvShows",
                newName: "Episode");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TvShows",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
