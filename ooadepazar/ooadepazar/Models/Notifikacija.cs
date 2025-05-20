using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models;

public class Notifikacija
{
    [Key]
    public int ID { get; set; }
    
    public string Sadrzaj { get; set; }
    public Stanje St { get; set; }
    public string Opis { get; set; }
    public float Cijena { get; set; }
    public bool Lokacija { get; set; }
    public DateTime DatumObjave { get; set; }
    
    [ForeignKey("Korisnik")]
    public int KorisnikId { get; set; }
}