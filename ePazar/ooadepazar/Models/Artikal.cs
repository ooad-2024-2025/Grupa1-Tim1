using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooadepazar.Models;

public class Artikal
{
    [Key]
    public int ID { get; set; }
    [Required(ErrorMessage = "Naziv je obavezan")]
    public string Naziv { get; set; }
    [Required(ErrorMessage = "Izaberite Stanje")]
    public Stanje Stanje { get; set; }
    [Required(ErrorMessage = "Opis je obavezan")]
    public string Opis { get; set; }
    [Required(ErrorMessage = "Cijena je obavezna")]
    public float Cijena { get; set; }
    public String? SlikaUrl { get; set; }
    public Kategorija Kategorija { get; set; }
    [Required(ErrorMessage = "Lokacija je obavezna.")]
    public string Lokacija { get; set; }
    public DateTime DatumObjave { get; set; }
    public DateTime DatumAzuriranja { get; set; }
    public Boolean Narucen { get; set; }
    public ApplicationUser? Korisnik { get; set; }
}