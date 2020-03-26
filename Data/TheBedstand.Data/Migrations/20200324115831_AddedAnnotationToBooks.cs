﻿
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBedstand.Data.Migrations
{
    public partial class AddedAnnotationToBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Annotation",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annotation",
                table: "Books");
        }
    }
}
