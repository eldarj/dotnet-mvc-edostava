using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eDostava.Data.Migrations
{
    public partial class restoranRecenzije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recenzija",
                table: "Lajkovi");

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    RestoranRecenzijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    NarucilacID = table.Column<int>(nullable: false),
                    Recenzija = table.Column<string>(nullable: true),
                    RestoranID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.RestoranRecenzijaID);
                    table.ForeignKey(
                        name: "FK_Recenzije_Narucioci_NarucilacID",
                        column: x => x.NarucilacID,
                        principalTable: "Narucioci",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_Restorani_RestoranID",
                        column: x => x.RestoranID,
                        principalTable: "Restorani",
                        principalColumn: "RestoranID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_NarucilacID",
                table: "Recenzije",
                column: "NarucilacID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_RestoranID",
                table: "Recenzije",
                column: "RestoranID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.AddColumn<string>(
                name: "Recenzija",
                table: "Lajkovi",
                nullable: true);
        }
    }
}
