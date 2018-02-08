﻿// <auto-generated />
using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace eDostava.Data.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20180208192947_changes2")]
    partial class changes2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDostava.Data.Models.Badge", b =>
                {
                    b.Property<int>("BadgeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojBodova");

                    b.Property<string>("Naziv");

                    b.HasKey("BadgeID");

                    b.ToTable("Badgevi");
                });

            modelBuilder.Entity("eDostava.Data.Models.Blok", b =>
                {
                    b.Property<int>("BlokID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GradID");

                    b.Property<string>("Naziv");

                    b.HasKey("BlokID");

                    b.HasIndex("GradID");

                    b.ToTable("Blokovi");
                });

            modelBuilder.Entity("eDostava.Data.Models.Grad", b =>
                {
                    b.Property<int>("GradID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.Property<int>("PoštanskiBroj");

                    b.HasKey("GradID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("eDostava.Data.Models.Hrana", b =>
                {
                    b.Property<int>("HranaID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<int>("JelovnikID");

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.Property<string>("Prilog");

                    b.Property<int>("Sifra");

                    b.Property<string>("Slika");

                    b.Property<int>("TipKuhinjeID");

                    b.HasKey("HranaID");

                    b.HasIndex("JelovnikID");

                    b.HasIndex("TipKuhinjeID");

                    b.ToTable("Proizvodi");
                });

            modelBuilder.Entity("eDostava.Data.Models.Jelovnik", b =>
                {
                    b.Property<int>("JelovnikID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktivan");

                    b.Property<string>("Opis");

                    b.Property<int>("RestoranID");

                    b.Property<string>("Slika");

                    b.HasKey("JelovnikID");

                    b.HasIndex("RestoranID");

                    b.ToTable("Jelovnici");
                });

            modelBuilder.Entity("eDostava.Data.Models.Kupon", b =>
                {
                    b.Property<int>("KuponID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Opis");

                    b.Property<double>("Vrijednost");

                    b.HasKey("KuponID");

                    b.ToTable("Kuponi");
                });

            modelBuilder.Entity("eDostava.Data.Models.Moderator", b =>
                {
                    b.Property<int>("KorisnikID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DatumKreiranja");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Permisije");

                    b.Property<string>("Username");

                    b.HasKey("KorisnikID");

                    b.ToTable("Moderatori");
                });

            modelBuilder.Entity("eDostava.Data.Models.Narucilac", b =>
                {
                    b.Property<int>("KorisnikID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BadgeID");

                    b.Property<int>("BlokID");

                    b.Property<DateTime?>("DatumKreiranja");

                    b.Property<string>("Email");

                    b.Property<string>("Ime");

                    b.Property<string>("Password");

                    b.Property<string>("Prezime");

                    b.Property<string>("Telefon");

                    b.Property<string>("Username");

                    b.HasKey("KorisnikID");

                    b.HasIndex("BadgeID");

                    b.HasIndex("BlokID");

                    b.ToTable("Narucioci");
                });

            modelBuilder.Entity("eDostava.Data.Models.Narudzba", b =>
                {
                    b.Property<int>("NarudzbaID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumVrijeme");

                    b.Property<int?>("KuponID");

                    b.Property<int>("Sifra");

                    b.Property<int>("Status");

                    b.Property<int>("UkupnaCijena");

                    b.HasKey("NarudzbaID");

                    b.HasIndex("KuponID");

                    b.ToTable("Narudzbe");
                });

            modelBuilder.Entity("eDostava.Data.Models.Obavjestenje", b =>
                {
                    b.Property<int>("ObavjestenjeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Poruka");

                    b.HasKey("ObavjestenjeID");

                    b.ToTable("Obavjestenja");
                });

            modelBuilder.Entity("eDostava.Data.Models.RadnoVrijeme", b =>
                {
                    b.Property<int>("RadnoVrijemeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Dan");

                    b.Property<int>("RestoranID");

                    b.Property<TimeSpan>("VrijemeOtvaranja");

                    b.Property<TimeSpan>("VrijemeZatvaranja");

                    b.HasKey("RadnoVrijemeID");

                    b.HasIndex("RestoranID");

                    b.ToTable("VrijemeRada");
                });

            modelBuilder.Entity("eDostava.Data.Models.Restoran", b =>
                {
                    b.Property<int>("RestoranID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlokID");

                    b.Property<int>("MinimalnaCijenaNarudžbe");

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.Property<string>("Slika");

                    b.Property<string>("Slogan");

                    b.Property<string>("Telefon");

                    b.Property<int>("VlasnikID");

                    b.Property<string>("WebUrl");

                    b.HasKey("RestoranID");

                    b.HasIndex("BlokID");

                    b.HasIndex("VlasnikID");

                    b.ToTable("Restorani");
                });

            modelBuilder.Entity("eDostava.Data.Models.RestoranLike", b =>
                {
                    b.Property<int>("RestoranLikeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NarucilacID");

                    b.Property<int>("RestoranID");

                    b.HasKey("RestoranLikeID");

                    b.HasIndex("NarucilacID");

                    b.HasIndex("RestoranID");

                    b.ToTable("Lajkovi");
                });

            modelBuilder.Entity("eDostava.Data.Models.StavkaNarudzbe", b =>
                {
                    b.Property<int>("StavkeNarudzbeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HranaID");

                    b.Property<int>("Kolicina");

                    b.Property<int>("NarudzbaID");

                    b.HasKey("StavkeNarudzbeID");

                    b.HasIndex("HranaID");

                    b.HasIndex("NarudzbaID");

                    b.ToTable("StavkeNarudzbe");
                });

            modelBuilder.Entity("eDostava.Data.Models.TipKuhinje", b =>
                {
                    b.Property<int>("TipKuhinjeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.HasKey("TipKuhinjeID");

                    b.ToTable("TipoviKuhinje");
                });

            modelBuilder.Entity("eDostava.Data.Models.Vlasnik", b =>
                {
                    b.Property<int>("KorisnikID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DatumKreiranja");

                    b.Property<string>("Email");

                    b.Property<string>("Ime");

                    b.Property<string>("Password");

                    b.Property<string>("Prezime");

                    b.Property<string>("Username");

                    b.HasKey("KorisnikID");

                    b.ToTable("Vlasnici");
                });

            modelBuilder.Entity("eDostava.Data.Models.Zalba", b =>
                {
                    b.Property<int>("ZalbaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Obrazlozenje");

                    b.Property<string>("Razlog");

                    b.HasKey("ZalbaID");

                    b.ToTable("Zalbe");
                });

            modelBuilder.Entity("eDostava.Data.Models.Blok", b =>
                {
                    b.HasOne("eDostava.Data.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDostava.Data.Models.Hrana", b =>
                {
                    b.HasOne("eDostava.Data.Models.Jelovnik", "Jelovnik")
                        .WithMany()
                        .HasForeignKey("JelovnikID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eDostava.Data.Models.TipKuhinje", "TipKuhinje")
                        .WithMany()
                        .HasForeignKey("TipKuhinjeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDostava.Data.Models.Jelovnik", b =>
                {
                    b.HasOne("eDostava.Data.Models.Restoran", "Restoran")
                        .WithMany()
                        .HasForeignKey("RestoranID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDostava.Data.Models.Narucilac", b =>
                {
                    b.HasOne("eDostava.Data.Models.Badge", "Badge")
                        .WithMany()
                        .HasForeignKey("BadgeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eDostava.Data.Models.Blok", "Blok")
                        .WithMany()
                        .HasForeignKey("BlokID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDostava.Data.Models.Narudzba", b =>
                {
                    b.HasOne("eDostava.Data.Models.Kupon", "Kupon")
                        .WithMany()
                        .HasForeignKey("KuponID");
                });

            modelBuilder.Entity("eDostava.Data.Models.RadnoVrijeme", b =>
                {
                    b.HasOne("eDostava.Data.Models.Restoran", "Restoran")
                        .WithMany()
                        .HasForeignKey("RestoranID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDostava.Data.Models.Restoran", b =>
                {
                    b.HasOne("eDostava.Data.Models.Blok", "Blok")
                        .WithMany()
                        .HasForeignKey("BlokID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eDostava.Data.Models.Vlasnik", "Vlasnik")
                        .WithMany()
                        .HasForeignKey("VlasnikID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDostava.Data.Models.RestoranLike", b =>
                {
                    b.HasOne("eDostava.Data.Models.Narucilac", "Narucilac")
                        .WithMany()
                        .HasForeignKey("NarucilacID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eDostava.Data.Models.Restoran", "Restoran")
                        .WithMany()
                        .HasForeignKey("RestoranID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("eDostava.Data.Models.StavkaNarudzbe", b =>
                {
                    b.HasOne("eDostava.Data.Models.Hrana", "Hrana")
                        .WithMany()
                        .HasForeignKey("HranaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eDostava.Data.Models.Narudzba", "Narudzba")
                        .WithMany()
                        .HasForeignKey("NarudzbaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
