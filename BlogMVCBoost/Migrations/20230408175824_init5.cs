using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogMVCBoost.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Articles",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Articles");
        }
    }
}
