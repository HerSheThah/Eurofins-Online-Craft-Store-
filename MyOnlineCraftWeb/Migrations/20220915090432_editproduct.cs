using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOnlineCraftWeb.Migrations
{
    public partial class editproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_productCategoryIdcatId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "productCategoryIdcatId",
                table: "Products",
                newName: "productCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_productCategoryIdcatId",
                table: "Products",
                newName: "IX_Products_productCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_productCategoryId",
                table: "Products",
                column: "productCategoryId",
                principalTable: "Categories",
                principalColumn: "catId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_productCategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "productCategoryId",
                table: "Products",
                newName: "productCategoryIdcatId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_productCategoryId",
                table: "Products",
                newName: "IX_Products_productCategoryIdcatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_productCategoryIdcatId",
                table: "Products",
                column: "productCategoryIdcatId",
                principalTable: "Categories",
                principalColumn: "catId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
