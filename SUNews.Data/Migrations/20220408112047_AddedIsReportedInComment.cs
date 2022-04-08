using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUNews.Data.Migrations
{
    public partial class AddedIsReportedInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Comments");
        }
    }
}
