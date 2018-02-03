using eDostava.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace eDostava.Data
{
    public class MojContext:DbContext
    {

        public MojContext(DbContextOptions<MojContext> options)
          : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<RestoranLike>()
           .HasOne(pt => pt.Restoran)
           .WithMany()
           .HasForeignKey(pt => pt.RestoranID)
           .OnDelete(DeleteBehavior.Restrict);
        }


        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Blok> Blokovi { get; set; }
        public DbSet<Hrana> Proizvodi { get; set; }
        public DbSet<Jelovnik> Jelovnici { get; set; }
        public DbSet<Kupon> Kuponi { get; set; }
        public DbSet<Moderator> Moderatori { get; set; }
        public DbSet<Narucilac> Narucioci { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<Obavjestenje> Obavjestenja { get; set; }
        public DbSet<RadnoVrijeme> VrijemeRada { get; set; }
        public DbSet<Restoran> Restorani { get; set; }
        public DbSet<RestoranLike> Lajkovi { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }
        public DbSet<Vlasnik> Vlasnici { get; set; }
        public DbSet<Zalba> Zalbe { get; set; }
        public DbSet<TipKuhinje> TipoviKuhinje { get; set; }
        public DbSet<Badge> Badgevi { get; set; }
      




    }
}
