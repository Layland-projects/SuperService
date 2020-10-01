using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperService_BackEnd.Migrations
{
    public partial class altered_itemIngredients_model_to_get_FKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientID",
                table: "ItemIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Items_ItemID",
                table: "ItemIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "ItemIngredients",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "ItemIngredients",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientID",
                table: "ItemIngredients",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Items_ItemID",
                table: "ItemIngredients",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientID",
                table: "ItemIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Items_ItemID",
                table: "ItemIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "ItemIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "ItemIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientID",
                table: "ItemIngredients",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Items_ItemID",
                table: "ItemIngredients",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
