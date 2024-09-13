using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kashmir.Captain.Server.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "id",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "id",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "id",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "id",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "id",
                table: "Users");
        }
    }
}
