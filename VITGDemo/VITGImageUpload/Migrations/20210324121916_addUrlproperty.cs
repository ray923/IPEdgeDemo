using Microsoft.EntityFrameworkCore.Migrations;

namespace VITGImageUpload.Migrations
{
    public partial class addUrlproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ImageInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "ImageInfos");
        }
    }
}
