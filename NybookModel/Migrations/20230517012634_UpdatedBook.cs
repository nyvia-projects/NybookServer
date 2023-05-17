using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NybookModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Book",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Book",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_authorId",
                table: "Book",
                column: "authorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_authorId",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Book",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Book",
                table: "Book",
                column: "authorId",
                principalTable: "Author",
                principalColumn: "Id");
        }
    }
}
