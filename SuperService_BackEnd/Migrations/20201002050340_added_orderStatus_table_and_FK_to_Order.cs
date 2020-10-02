using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperService_BackEnd.Migrations
{
    public partial class added_orderStatus_table_and_FK_to_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatusID",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrderStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusID",
                table: "Orders",
                column: "OrderStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusID",
                table: "Orders",
                column: "OrderStatusID",
                principalTable: "OrderStatus",
                principalColumn: "OrderStatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("Insert into OrderStatus " +
                "Values('Order Placed', 'A new order has been placed and has not yet been assigned.'), " +
                "('In Process', 'An order is currently being prepared in the kitchen.'), " +
                "('Ready To Collect', 'An order has been prepared and is ready to collect'), " +
                "('Completed', 'An order has been taken to the customer')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusID",
                table: "Orders");
        }
    }
}
