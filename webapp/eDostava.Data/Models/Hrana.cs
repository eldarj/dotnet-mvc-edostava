using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
    public class Hrana
    {
        [Key]
        public int HranaID { get; set; }
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public string Opis { get; set; }
        public string Slika { get; set; }
        public string Prilog { get; set; }
        [ForeignKey("TipKuhinje")]
        public int TipKuhinjeID { get; set; }
        public TipKuhinje TipKuhinje { get; set; }
        [ForeignKey("Jelovnik")]
        public int JelovnikID { get; set; }
        public Jelovnik Jelovnik { get; set; }

        public virtual ICollection<HranaPrilog> Prilozi { get; set; }
        public virtual ICollection<HranaPrilog> PrilogOd { get; set; }
    }

    public class HranaPrilog
    {
        public int HranaID { get; set; }
        public int PrilogID { get; set; }
        public virtual Hrana Hrana { get; set; }
        public virtual Hrana Prilog { get; set; }
    }
}

