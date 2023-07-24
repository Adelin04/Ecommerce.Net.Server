using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Data.Migrations
{
    public partial class added_AddressCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomer_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressCustomer",
                table: "AddressCustomer");

            migrationBuilder.RenameTable(
                name: "AddressCustomer",
                newName: "AddressCustomers");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AddressCustomers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressCustomers",
                table: "AddressCustomers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_AddressCustomers_UserId",
                table: "AddressCustomers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressCustomers_Users_UserId",
                table: "AddressCustomers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerid",
                table: "Invoices",
                column: "AddressCustomerid",
                principalTable: "AddressCustomers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressCustomers_Users_UserId",
                table: "AddressCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AddressCustomers_AddressCustomerid",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressCustomers",
                table: "AddressCustomers");

            migrationBuilder.DropIndex(
                name: "IX_AddressCustomers_UserId",
                table: "AddressCustomers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AddressCustomers");

            migrationBuilder.RenameTable(
                name: "AddressCustomers",
                newName: "AddressCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressCustomer",
                table: "AddressCustomer",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AddressCustomer_AddressCustomerid",
                table: "Invoices",
                column: "AddressCustomerid",
                principalTable: "AddressCustomer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
