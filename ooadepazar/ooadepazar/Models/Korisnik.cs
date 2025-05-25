using System.ComponentModel.DataAnnotations;

namespace ooadepazar.Models;

public class Korisnik
{
    [Key]
    public int ID { get; set; }

    /*
    public string Ime { get; set; }

    public string Prezime { get; set; }

    public string Adresa { get; set; }
    public string EmailAdresa { get; set; }
    public string BrojTelefona { get; set; }

    public Uloga Uloga { get; set; } // Admin, Kurirska_Sluzba, Korisnik

    public DateTime DatumRegistracije { get; set; } = DateTime.Now;

    public string Sifra{ get; set; }

    public string ? KurirskaSluzba { get; set; }
    */

}