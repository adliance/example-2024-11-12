using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailHashAndSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailHash",
                table: "Registrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailHashSalt",
                table: "Registrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailHash",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "EmailHashSalt",
                table: "Registrations");
        }
    }
}
