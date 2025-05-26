using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models
{
    public class Pracenje
    {
        [Key]
        public int ID { get; set; }

        public int? PraceniID { get; set; }
        public Korisnik? PraceniKorisnik { get; set; }
        public int? PratilacID { get; set; }
        public Korisnik? PratilacKorisnik { get; set; }
    }
}
