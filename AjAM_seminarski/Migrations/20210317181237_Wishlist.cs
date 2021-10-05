using Microsoft.EntityFrameworkCore.Migrations;

namespace AjAM_seminarski.Migrations
{
    public partial class Wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Cars_CarId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ClientId",
                table: "Wishlists");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Wishlists",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Wishlists",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "Wishlists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ClientId1",
                table: "Wishlists",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Cars_CarId",
                table: "Wishlists",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId1",
                table: "Wishlists",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Cars_CarId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId1",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ClientId1",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Wishlists");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Wishlists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Wishlists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ClientId",
                table: "Wishlists",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Cars_CarId",
                table: "Wishlists",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId",
                table: "Wishlists",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
