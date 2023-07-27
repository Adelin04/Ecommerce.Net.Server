using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Data.Migrations
{
    public partial class updated2_invoice_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AddressCustomerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AddressCustomerId",
                table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AddressCustomerId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AddressCustomerId",
                table: "Invoices",
                column: "AddressCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerId",
                table: "Invoices",
                column: "AddressCustomerId",
                principalTable: "AddressCustomers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
