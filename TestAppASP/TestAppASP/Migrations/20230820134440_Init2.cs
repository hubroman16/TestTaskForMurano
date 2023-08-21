using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAppASP.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchRequest",
                table: "SearchRequest");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "SearchRequest",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchRequest",
                table: "SearchRequest",
                columns: new[] { "Request", "URL" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchRequest",
                table: "SearchRequest");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "SearchRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchRequest",
                table: "SearchRequest",
                column: "Request");
        }
    }
}
