using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models;

public class Narudzba
{
    [Key]

    public int ID { get; set; }

    [Required]
    public DateTime DatumNarudzbe { get; set; }

    [Required]
    public DateTime ? DatumObrade { get; set; }

    [Required]  
    public Status Status { get; set; }  // Kreiran, UObradi, Dostavljen

    [ForeignKey("KorisnikID")]
    public int KorisnikID { get; set; }

    [ForeignKey("ArtikalID")]
    public int ArtikalID { get; set; }

    [ForeignKey("KurirskasluzbaID")]
    public int ? KurirskasluzbaID { get; set; }
}