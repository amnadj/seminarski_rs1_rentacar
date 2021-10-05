using Microsoft.EntityFrameworkCore.Migrations;

namespace AjAM_seminarski.Migrations
{
    public partial class Contract_Car : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarIdd",
                table: "Contracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarIdd",
                table: "Contracts");
        }
    }
}
