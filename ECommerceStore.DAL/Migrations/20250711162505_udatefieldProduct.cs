using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class udatefieldProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Products",
                newName: "ImageUrl");
        }
    }
}
