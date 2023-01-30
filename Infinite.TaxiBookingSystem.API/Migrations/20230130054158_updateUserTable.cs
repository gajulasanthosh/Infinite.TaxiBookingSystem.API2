using Microsoft.EntityFrameworkCore.Migrations;

namespace Infinite.TaxiBookingSystem.API.Migrations
{
    public partial class updateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Customers_CustomerID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomerID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeID",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerID",
                table: "Users",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                unique: true,
                filter: "[EmployeeID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Customers_CustomerID",
                table: "Users",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Customers_CustomerID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomerID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeID",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerID",
                table: "Users",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeID",
                table: "Users",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Customers_CustomerID",
                table: "Users",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
