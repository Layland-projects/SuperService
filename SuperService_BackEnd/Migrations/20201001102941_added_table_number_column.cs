using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperService_BackEnd.Migrations
{
    public partial class added_table_number_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableNumber",
                table: "Tables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "Tables");
        }
    }
}
