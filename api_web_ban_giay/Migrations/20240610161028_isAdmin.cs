using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_web_ban_giay.Migrations
{
    /// <inheritdoc />
    public partial class isAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "TaiKhoan",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "TaiKhoan");
        }
    }
}
