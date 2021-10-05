using Microsoft.EntityFrameworkCore.Migrations;

namespace AjAM_seminarski.Migrations
{
    public partial class Contract_Cars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CarId",
                table: "Contracts",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Cars_CarId",
                table: "Contracts",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Cars_CarId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CarId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Contracts");
        }
    }
}
