using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models
{
    public class Pracenje
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("PraceniID")]
        public int PraceniID { get; set; }

        [ForeignKey("PratilacID")]
        public int PratilacID { get; set; }

    }
}
