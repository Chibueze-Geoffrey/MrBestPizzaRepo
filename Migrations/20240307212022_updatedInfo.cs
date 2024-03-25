using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrBestPizza.Migrations
{
    /// <inheritdoc />
    public partial class updatedInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Small pizza served as snack.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Small pizza served as an hors d'oeuvre or snack.");
        }
    }
}
