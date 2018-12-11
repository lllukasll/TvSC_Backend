using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class addedFirstPartAndSecondPartForNotificationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstPart",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPart",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPart",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SecondPart",
                table: "Notifications");
        }
    }
}
