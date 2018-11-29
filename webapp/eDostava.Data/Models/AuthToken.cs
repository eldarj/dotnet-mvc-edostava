using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eDostava.Data.Models
{
    public class AuthToken
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime DatumGenerisanja { get; set; }
        public string Ip { get; set; }

        // FKs - check this
        [ForeignKey(nameof(VlasnikId))]
        public int? VlasnikId { get; set; }
        public virtual Vlasnik Vlasnik { get; set; }

        [ForeignKey(nameof(NarucilacId))]
        public int? NarucilacId { get; set; }
        public virtual Narucilac Narucilac { get; set; }

        [ForeignKey(nameof(ModeratorId))]
        public int? ModeratorId { get; set; }
        public virtual Moderator Moderator { get; set; }

        // check this
        public string GetKorisnikUsername()
        {
            foreach (var item in new List<Korisnik> { (Korisnik)Vlasnik, (Korisnik)Narucilac, (Korisnik)Moderator })
            {
                if (item != null)
                {
                    return item.Username;
                }
            }
            return "-";
        }

    }
}
