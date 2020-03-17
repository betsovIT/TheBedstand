﻿// <auto-generated/>
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBedstand.Data.Migrations
{
    public partial class ChangesToAuthorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "PersonalName",
                table: "Authors",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Authors",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
