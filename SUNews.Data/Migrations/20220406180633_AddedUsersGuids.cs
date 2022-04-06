using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUNews.Data.Migrations
{
    public partial class AddedUsersGuids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Articles",
                type: "float",
                nullable: true);
        }
    }
}
