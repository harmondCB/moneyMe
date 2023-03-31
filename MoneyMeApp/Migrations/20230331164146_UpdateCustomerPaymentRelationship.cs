using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMeApp.Migrations
{
    public partial class UpdateCustomerPaymentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerPaymentProducts_CustomerPaymentId",
                table: "CustomerPaymentProducts");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentProducts_CustomerPaymentId",
                table: "CustomerPaymentProducts",
                column: "CustomerPaymentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerPaymentProducts_CustomerPaymentId",
                table: "CustomerPaymentProducts");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentProducts_CustomerPaymentId",
                table: "CustomerPaymentProducts",
                column: "CustomerPaymentId");
        }
    }
}
