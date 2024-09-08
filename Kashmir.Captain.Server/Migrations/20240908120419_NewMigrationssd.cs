using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kashmir.Captain.Server.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationssd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "id",
                table: "UserRoles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId",
                schema: "id",
                table: "UserRoles");
        }
    }
}
