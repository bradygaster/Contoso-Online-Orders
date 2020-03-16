using Microsoft.EntityFrameworkCore.Migrations;

namespace Contoso.Online.Orders.Server.Migrations
{
    public partial class changedefaultschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "contoso");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "contoso");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Order",
                newSchema: "contoso");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItem",
                newSchema: "contoso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                schema: "contoso",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "contoso",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "CartItem",
                schema: "contoso",
                newName: "CartItem");
        }
    }
}
