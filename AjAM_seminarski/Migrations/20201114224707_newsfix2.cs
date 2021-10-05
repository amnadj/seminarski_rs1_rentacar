using Microsoft.EntityFrameworkCore.Migrations;

namespace AjAM_seminarski.Migrations
{
    public partial class newsfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "News",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "News");

        }
    }
}
