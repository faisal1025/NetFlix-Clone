using Microsoft.EntityFrameworkCore.Migrations;

namespace NetChill.Project.DataAccess.Data.Migrations
{
    public partial class moviestableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentPath",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MoviePoster",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "videoName",
                table: "Movies",
                newName: "VideoName");

            migrationBuilder.RenameColumn(
                name: "isFeatured",
                table: "Movies",
                newName: "IsFeatured");

            migrationBuilder.RenameColumn(
                name: "imageName",
                table: "Movies",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoName",
                table: "Movies",
                newName: "videoName");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "Movies",
                newName: "isFeatured");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Movies",
                newName: "imageName");

            migrationBuilder.AddColumn<string>(
                name: "ContentPath",
                table: "Movies",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoviePoster",
                table: "Movies",
                type: "nvarchar(MAX)",
                nullable: true);
        }
    }
}
