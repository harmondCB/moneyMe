using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMeApp.Migrations
{
    public partial class UpdateProductModelV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterestFree",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "InterestRate",
                table: "Products",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InterestRate",
                value: 0.05m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InterestRate",
                value: 0.05m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsInterestFree",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsInterestFree",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsInterestFree",
                value: true);
        }
    }
}
