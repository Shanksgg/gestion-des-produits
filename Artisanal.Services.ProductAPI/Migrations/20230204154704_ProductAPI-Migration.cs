using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artisanal.Services.ProductAPI.Migrations
{
    public partial class ProductAPIMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "ImageURL", "Price", "ProductName" },
                values: new object[] { 1, "categorie1", "1.jpg", 15.0, "ChaiseBoisMassif" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "ImageURL", "Price", "ProductName" },
                values: new object[] { 2, "categorie2", "2.jpg", 15.0, "RangementBoisMassif" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "ImageURL", "Price", "ProductName" },
                values: new object[] { 3, "categorie1", "3.jpg", 15.0, "DecorArtis" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
