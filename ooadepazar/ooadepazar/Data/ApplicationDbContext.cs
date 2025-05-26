using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Models;

namespace ooadepazar.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Artikal> Artikal { get; set; }
    public DbSet<Korisnik> Korisnik { get; set; }
    public DbSet<Narudzba> Narudzba { get; set; }
    public DbSet<Notifikacija> Notifikacija { get; set; }
    public DbSet<Pracenje> Pracenje { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table mappings (already present, keep them)
        modelBuilder.Entity<Artikal>().ToTable("Artikal");
        modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
        modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
        modelBuilder.Entity<Notifikacija>().ToTable("Notifikacija");
        modelBuilder.Entity<Pracenje>().ToTable("Pracenje");

        // --- IMPORTANT: Add these relationship configurations ---

        // Configure the relationship for Narudzba.Korisnik (the customer who placed the order)
        modelBuilder.Entity<Narudzba>()
            .HasOne(n => n.Korisnik) // Narudzba has one Korisnik
            .WithMany()              // Korisnik has many Narudzbe (but we're not defining a navigation property on Korisnik for orders)
            .HasForeignKey(n => n.KorisnikID) // The foreign key property
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for this relationship

        // Configure the relationship for Narudzba.KurirskaSluzba (the courier service)
        // This is the second foreign key to Korisnik and causes the conflict.
        modelBuilder.Entity<Narudzba>()
            .HasOne(n => n.KurirskaSluzba) // Narudzba has one KurirskaSluzba (which is a Korisnik)
            .WithMany()                    // Korisnik has many Narudzbe as a courier (again, no navigation property on Korisnik)
            .HasForeignKey(n => n.KurirskaSluzbaID) // The foreign key property
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for this relationship

        // Configure the relationship for Narudzba.Artikal
        modelBuilder.Entity<Narudzba>()
            .HasOne(n => n.Artikal)
            .WithMany()
            .HasForeignKey(n => n.ArtikalID)
            .OnDelete(DeleteBehavior.Restrict); // Also set this for Artikal to be safe, though it might not cause a direct cycle.

        // Make sure to call the base method for IdentityDbContext configurations
        base.OnModelCreating(modelBuilder);
    }
}