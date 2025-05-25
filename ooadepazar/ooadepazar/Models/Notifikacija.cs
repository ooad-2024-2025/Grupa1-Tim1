using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models;

public class Notifikacija
{
    [Key]
    public int ID { get; set; }
    
    public string Sadrzaj { get; set; }
    public DateTime DatumObjave { get; set; }
    public bool Procitana { get; set; } = false;

    [ForeignKey("Korisnik")]
    public int KorisnikId { get; set; }
}