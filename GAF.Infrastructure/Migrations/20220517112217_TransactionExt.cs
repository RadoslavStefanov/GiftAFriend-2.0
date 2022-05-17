using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftAFriend_2._0.Data.Migrations
{
    public partial class TransactionExt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "TransferEvents",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransferEvents");
        }
    }
}
