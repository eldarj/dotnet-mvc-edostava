using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eDostava.Data.Migrations
{
    public partial class updatehraneipriloga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sifra",
                table: "Narudzbe");

            migrationBuilder.AddColumn<Guid>(
                name: "Sifra",
                table: "Narudzbe",
                nullable: false
                );

            migrationBuilder.CreateTable(
                name: "HranaPrilog",
                columns: table => new
                {
                    HranaID = table.Column<int>(nullable: false),
                    PrilogID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HranaPrilog", x => new { x.HranaID, x.PrilogID });
                    table.ForeignKey(
                        name: "FK_HranaPrilog_Proizvodi_HranaID",
                        column: x => x.HranaID,
                        principalTable: "Proizvodi",
                        principalColumn: "HranaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HranaPrilog_Proizvodi_PrilogID",
                        column: x => x.PrilogID,
                        principalTable: "Proizvodi",
                        principalColumn: "HranaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HranaPrilog_PrilogID",
                table: "HranaPrilog",
                column: "PrilogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HranaPrilog");

            migrationBuilder.DropColumn(
                name: "Sifra",
                table: "Narudzbe");

            migrationBuilder.AddColumn<int>(
                name: "Sifra",
                table: "Narudzbe",
                nullable: false
                );
        }
    }
}
