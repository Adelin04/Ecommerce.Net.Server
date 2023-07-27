using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Data.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "AddressCustomerid",
                table: "Invoices",
                newName: "AddressCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_AddressCustomerid",
                table: "Invoices",
                newName: "IX_Invoices_AddressCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerId",
                table: "Invoices",
                column: "AddressCustomerId",
                principalTable: "AddressCustomers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "AddressCustomerId",
                table: "Invoices",
                newName: "AddressCustomerid");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_AddressCustomerId",
                table: "Invoices",
                newName: "IX_Invoices_AddressCustomerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerid",
                table: "Invoices",
                column: "AddressCustomerid",
                principalTable: "AddressCustomers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
