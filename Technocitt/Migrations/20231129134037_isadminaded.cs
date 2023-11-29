using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technocitt.Migrations
{
    /// <inheritdoc />
    public partial class isadminaded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "TechnoUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "TechnoUsers");
        }
    }
}
