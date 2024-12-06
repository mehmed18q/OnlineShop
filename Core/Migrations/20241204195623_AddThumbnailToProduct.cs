using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                schema: "Base",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileExtension",
                schema: "Base",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileName",
                schema: "Base",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailFileSize",
                schema: "Base",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                schema: "Base",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileExtension",
                schema: "Base",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileName",
                schema: "Base",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileSize",
                schema: "Base",
                table: "Products");
        }
    }
}
