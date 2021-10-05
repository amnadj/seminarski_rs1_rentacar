using Microsoft.EntityFrameworkCore.Migrations;

namespace AjAM_seminarski.Migrations
{
    public partial class contractfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Rents_RentId",
                table: "Contracts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b870def9-06ef-4a62-871c-3ff99bcf2770");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df7205d5-bdcd-4107-85ba-846c762f743b");

            migrationBuilder.AlterColumn<int>(
                name: "RentId",
                table: "Contracts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ca297e5-f561-412c-b0ef-2e29818d49ee", "b590ac87-aa8e-4ff3-9b62-cda9fce663c8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b926b4dd-9098-4957-9dca-cbe8be4cd38b", "1903b32e-4318-46f9-9671-d52214a2ca8f", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Rents_RentId",
                table: "Contracts",
                column: "RentId",
                principalTable: "Rents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Rents_RentId",
                table: "Contracts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ca297e5-f561-412c-b0ef-2e29818d49ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b926b4dd-9098-4957-9dca-cbe8be4cd38b");

            migrationBuilder.AlterColumn<int>(
                name: "RentId",
                table: "Contracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b870def9-06ef-4a62-871c-3ff99bcf2770", "129c9b83-a4e0-4ae4-aa0c-3702cefb231f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df7205d5-bdcd-4107-85ba-846c762f743b", "1702a318-96bf-4683-8660-e14eae2f1334", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Rents_RentId",
                table: "Contracts",
                column: "RentId",
                principalTable: "Rents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
