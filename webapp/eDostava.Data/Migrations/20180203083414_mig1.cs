using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eDostava.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badgevi",
                columns: table => new
                {
                    BadgeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojBodova = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badgevi", x => x.BadgeID);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    GradID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    PoštanskiBroj = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.GradID);
                });

            migrationBuilder.CreateTable(
                name: "Kuponi",
                columns: table => new
                {
                    KuponID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true),
                    Vrijednost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuponi", x => x.KuponID);
                });

            migrationBuilder.CreateTable(
                name: "Moderatori",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Permisije = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderatori", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Obavjestenja",
                columns: table => new
                {
                    ObavjestenjeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Poruka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavjestenja", x => x.ObavjestenjeID);
                });

            migrationBuilder.CreateTable(
                name: "TipoviKuhinje",
                columns: table => new
                {
                    TipKuhinjeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviKuhinje", x => x.TipKuhinjeID);
                });

            migrationBuilder.CreateTable(
                name: "Vlasnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlasnici", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Zalbe",
                columns: table => new
                {
                    ZalbaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Obrazlozenje = table.Column<string>(nullable: true),
                    Razlog = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalbe", x => x.ZalbaID);
                });

            migrationBuilder.CreateTable(
                name: "Blokovi",
                columns: table => new
                {
                    BlokID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GradID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blokovi", x => x.BlokID);
                    table.ForeignKey(
                        name: "FK_Blokovi_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Narudzbe",
                columns: table => new
                {
                    NarudzbaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumVrijeme = table.Column<DateTime>(nullable: false),
                    KuponID = table.Column<int>(nullable: true),
                    Sifra = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UkupnaCijena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzbe", x => x.NarudzbaID);
                    table.ForeignKey(
                        name: "FK_Narudzbe_Kuponi_KuponID",
                        column: x => x.KuponID,
                        principalTable: "Kuponi",
                        principalColumn: "KuponID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Narucioci",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BadgeID = table.Column<int>(nullable: false),
                    BlokID = table.Column<int>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narucioci", x => x.KorisnikID);
                    table.ForeignKey(
                        name: "FK_Narucioci_Badgevi_BadgeID",
                        column: x => x.BadgeID,
                        principalTable: "Badgevi",
                        principalColumn: "BadgeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narucioci_Blokovi_BlokID",
                        column: x => x.BlokID,
                        principalTable: "Blokovi",
                        principalColumn: "BlokID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restorani",
                columns: table => new
                {
                    RestoranID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlokID = table.Column<int>(nullable: false),
                    MinimalnaCijenaNarudžbe = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Slika = table.Column<string>(nullable: true),
                    Slogan = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    VlasnikID = table.Column<int>(nullable: false),
                    WebUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restorani", x => x.RestoranID);
                    table.ForeignKey(
                        name: "FK_Restorani_Blokovi_BlokID",
                        column: x => x.BlokID,
                        principalTable: "Blokovi",
                        principalColumn: "BlokID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restorani_Vlasnici_VlasnikID",
                        column: x => x.VlasnikID,
                        principalTable: "Vlasnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jelovnici",
                columns: table => new
                {
                    JelovnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktivan = table.Column<bool>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    RestoranID = table.Column<int>(nullable: false),
                    Slika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jelovnici", x => x.JelovnikID);
                    table.ForeignKey(
                        name: "FK_Jelovnici_Restorani_RestoranID",
                        column: x => x.RestoranID,
                        principalTable: "Restorani",
                        principalColumn: "RestoranID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lajkovi",
                columns: table => new
                {
                    RestoranLikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NarucilacID = table.Column<int>(nullable: false),
                    RestoranID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lajkovi", x => x.RestoranLikeID);
                    table.ForeignKey(
                        name: "FK_Lajkovi_Narucioci_NarucilacID",
                        column: x => x.NarucilacID,
                        principalTable: "Narucioci",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lajkovi_Restorani_RestoranID",
                        column: x => x.RestoranID,
                        principalTable: "Restorani",
                        principalColumn: "RestoranID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VrijemeRada",
                columns: table => new
                {
                    RadnoVrijemeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dan = table.Column<int>(nullable: false),
                    RestoranID = table.Column<int>(nullable: false),
                    VrijemeOtvaranja = table.Column<TimeSpan>(nullable: false),
                    VrijemeZatvaranja = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrijemeRada", x => x.RadnoVrijemeID);
                    table.ForeignKey(
                        name: "FK_VrijemeRada_Restorani_RestoranID",
                        column: x => x.RestoranID,
                        principalTable: "Restorani",
                        principalColumn: "RestoranID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    HranaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<double>(nullable: false),
                    JelovnikID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Prilog = table.Column<string>(nullable: true),
                    Sifra = table.Column<int>(nullable: false),
                    Slika = table.Column<string>(nullable: true),
                    TipKuhinjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.HranaID);
                    table.ForeignKey(
                        name: "FK_Proizvodi_Jelovnici_JelovnikID",
                        column: x => x.JelovnikID,
                        principalTable: "Jelovnici",
                        principalColumn: "JelovnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvodi_TipoviKuhinje_TipKuhinjeID",
                        column: x => x.TipKuhinjeID,
                        principalTable: "TipoviKuhinje",
                        principalColumn: "TipKuhinjeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeNarudzbe",
                columns: table => new
                {
                    StavkeNarudzbeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HranaID = table.Column<int>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    NarudzbaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeNarudzbe", x => x.StavkeNarudzbeID);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Proizvodi_HranaID",
                        column: x => x.HranaID,
                        principalTable: "Proizvodi",
                        principalColumn: "HranaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Narudzbe_NarudzbaID",
                        column: x => x.NarudzbaID,
                        principalTable: "Narudzbe",
                        principalColumn: "NarudzbaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blokovi_GradID",
                table: "Blokovi",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Jelovnici_RestoranID",
                table: "Jelovnici",
                column: "RestoranID");

            migrationBuilder.CreateIndex(
                name: "IX_Lajkovi_NarucilacID",
                table: "Lajkovi",
                column: "NarucilacID");

            migrationBuilder.CreateIndex(
                name: "IX_Lajkovi_RestoranID",
                table: "Lajkovi",
                column: "RestoranID");

            migrationBuilder.CreateIndex(
                name: "IX_Narucioci_BadgeID",
                table: "Narucioci",
                column: "BadgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Narucioci_BlokID",
                table: "Narucioci",
                column: "BlokID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_KuponID",
                table: "Narudzbe",
                column: "KuponID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_JelovnikID",
                table: "Proizvodi",
                column: "JelovnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_TipKuhinjeID",
                table: "Proizvodi",
                column: "TipKuhinjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Restorani_BlokID",
                table: "Restorani",
                column: "BlokID");

            migrationBuilder.CreateIndex(
                name: "IX_Restorani_VlasnikID",
                table: "Restorani",
                column: "VlasnikID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_HranaID",
                table: "StavkeNarudzbe",
                column: "HranaID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_NarudzbaID",
                table: "StavkeNarudzbe",
                column: "NarudzbaID");

            migrationBuilder.CreateIndex(
                name: "IX_VrijemeRada_RestoranID",
                table: "VrijemeRada",
                column: "RestoranID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lajkovi");

            migrationBuilder.DropTable(
                name: "Moderatori");

            migrationBuilder.DropTable(
                name: "Obavjestenja");

            migrationBuilder.DropTable(
                name: "StavkeNarudzbe");

            migrationBuilder.DropTable(
                name: "VrijemeRada");

            migrationBuilder.DropTable(
                name: "Zalbe");

            migrationBuilder.DropTable(
                name: "Narucioci");

            migrationBuilder.DropTable(
                name: "Proizvodi");

            migrationBuilder.DropTable(
                name: "Narudzbe");

            migrationBuilder.DropTable(
                name: "Badgevi");

            migrationBuilder.DropTable(
                name: "Jelovnici");

            migrationBuilder.DropTable(
                name: "TipoviKuhinje");

            migrationBuilder.DropTable(
                name: "Kuponi");

            migrationBuilder.DropTable(
                name: "Restorani");

            migrationBuilder.DropTable(
                name: "Blokovi");

            migrationBuilder.DropTable(
                name: "Vlasnici");

            migrationBuilder.DropTable(
                name: "Gradovi");
        }
    }
}
