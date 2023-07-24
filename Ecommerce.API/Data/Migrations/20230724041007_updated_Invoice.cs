using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Data.Migrations
{
    public partial class updated_Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomer_Addressid",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Addressid",
                table: "Invoices",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_Addressid",
                table: "Invoices",
                newName: "IX_Invoices_UserId");

            migrationBuilder.AddColumn<long>(
                name: "AddressCustomerid",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AddressCustomerid",
                table: "Invoices",
                column: "AddressCustomerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomer_AddressCustomerid",
                table: "Invoices",
                column: "AddressCustomerid",
                principalTable: "AddressCustomer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_UserId",
                table: "Invoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomer_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_UserId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AddressCustomerid",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Invoices",
                newName: "Addressid");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                newName: "IX_Invoices_Addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomer_Addressid",
                table: "Invoices",
                column: "Addressid",
                principalTable: "AddressCustomer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
