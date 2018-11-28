using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class ChangedAssignmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "ActorsAssignments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "ActorsAssignments");
        }
    }
}
