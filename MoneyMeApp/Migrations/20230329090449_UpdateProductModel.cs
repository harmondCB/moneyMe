using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMeApp.Migrations
{
    public partial class UpdateProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterestFreeMonth",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterestFree",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestFreeMonth",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsInterestFree",
                table: "Products");
        }
    }
}
