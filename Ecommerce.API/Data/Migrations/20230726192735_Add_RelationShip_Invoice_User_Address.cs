using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Data.Migrations
{
    public partial class Add_RelationShip_Invoice_User_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AddressCustomerid",
                table: "Invoices");

            migrationBuilder.AddColumn<long>(
                name: "Invoiceid",
                table: "AddressCustomers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressCustomers_Invoiceid",
                table: "AddressCustomers",
                column: "Invoiceid");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressCustomers_Invoices_Invoiceid",
                table: "AddressCustomers",
                column: "Invoiceid",
                principalTable: "Invoices",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressCustomers_Invoices_Invoiceid",
                table: "AddressCustomers");

            migrationBuilder.DropIndex(
                name: "IX_AddressCustomers_Invoiceid",
                table: "AddressCustomers");

            migrationBuilder.DropColumn(
                name: "Invoiceid",
                table: "AddressCustomers");

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
                name: "FK_Invoices_AddressCustomers_AddressCustomerid",
                table: "Invoices",
                column: "AddressCustomerid",
                principalTable: "AddressCustomers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
