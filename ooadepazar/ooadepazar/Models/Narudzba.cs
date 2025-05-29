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

    public ApplicationUser KorisnikId { get; set; }
    
    public int? ArtikalID { get; set; }
    public Artikal? Artikal { get; set; }
    
    public ApplicationUser KurirskaSluzbaId { get; set; }
    
}