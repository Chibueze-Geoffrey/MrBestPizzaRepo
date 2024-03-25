using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MrBestPizza.Migrations
{
    /// <inheritdoc />
    public partial class newData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.PizzaId);
                    table.ForeignKey(
                        name: "FK_Pizzas_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Pizza folded in half turnover-style.", "Calzone" },
                    { 2, "Small pizza served as an hors d'oeuvre or snack.", "Pizzetta" },
                    { 3, "The pizza is deep-fried (cooked in oil) instead of baked.", "Deep-fried pizza " },
                    { 4, "Distinguished by the use of non-traditional ingredients, especially varieties of fresh produce.", "California-style pizza" }
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "PizzaId", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 101, 1, "A Pizza that enriches the soul", "Chicken Pizza", 29m },
                    { 122, 2, "A Pocket friendly Pizza", "Beef", 29.7m },
                    { 127, 4, "Specially made for you", "Spicy Pizza", 50m },
                    { 324, 3, "A blend of vegetables", "Vegie Pizza", 46.5m },
                    { 453, 2, "Pizza de French", "French Pizza", 45.6m },
                    { 1009, 1, "No beef Just plain", "Plain ", 34.8m },
                    { 1123, 4, "A blend of nutrient", "Seasoned", 23.8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_CategoryId",
                table: "Pizzas",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
