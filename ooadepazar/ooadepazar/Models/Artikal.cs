using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models;

public class Artikal
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Naziv { get; set; }
    [Required]
    public Stanje Stanje { get; set; }
    [Required]
    public string Opis { get; set; }
    [Required]
    public float Cijena { get; set; }
    [Required]
    public string Lokacija { get; set; }
    public DateTime DatumObjave { get; set; }
    
    public int KorisnikId { get; set; }
    public Korisnik? Korisnik { get; set; }
    
}