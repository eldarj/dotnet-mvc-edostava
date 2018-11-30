using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eDostava.Data.Migrations
{
    public partial class narucilacImageUrlGradDrzava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Narucioci",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Drzava",
                table: "Gradovi",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Narucioci");

            migrationBuilder.DropColumn(
                name: "Drzava",
                table: "Gradovi");
        }
    }
}
