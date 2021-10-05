using Microsoft.EntityFrameworkCore.Migrations;

namespace AjAM_seminarski.Migrations
{
    public partial class stringUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ClientId",
                table: "Wishlists",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId",
                table: "Wishlists",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ClientId",
                table: "Wishlists");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Wishlists",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "Wishlists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ClientId1",
                table: "Wishlists",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_ClientId1",
                table: "Wishlists",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
