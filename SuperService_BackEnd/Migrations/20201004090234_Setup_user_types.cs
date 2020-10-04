using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperService_BackEnd.Migrations
{
    public partial class Setup_user_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeID",
                table: "Users"
                );
            migrationBuilder.Sql(
                "Truncate Table UserTypes;\n" +
                "GO \n"
                );
            migrationBuilder.Sql("Insert Into UserTypes \n" +
                "Values('Admin', 'System Administrator'), \n" +
                "('Kitchen staff', 'Kitchen worker'), \n" +
                "('Server', 'Front of house worker');\n"
                );
            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeID",
                table: "Users",
                column: "UserTypeID",
                principalTable: "UserTypes",
                principalColumn: "UserTypeID"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
