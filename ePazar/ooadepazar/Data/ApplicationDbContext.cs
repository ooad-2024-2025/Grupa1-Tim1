﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Models;

namespace ooadepazar.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Artikal> Artikal { get; set; }
    public DbSet<Narudzba> Narudzba { get; set; }
    public DbSet<Notifikacija> Notifikacija { get; set; }
    public DbSet<Pracenje> Pracenje { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artikal>().ToTable("Artikal");
        modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
        modelBuilder.Entity<Notifikacija>().ToTable("Notifikacija");
        modelBuilder.Entity<Pracenje>().ToTable("Pracenje");
        
        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.Property(u => u.Ime);
            b.Property(u => u.Prezime);
            b.Property(u => u.Adresa);
            b.Property(u => u.BrojTelefona);
            b.Property(u => u.Uloga);
            b.Property(u => u.EmailAdresa);
            b.Property(u => u.DatumRegistracije);
            b.Property(u => u.KurirskaSluzba);
        });

        // Artikal → ApplicationUser (Korisnik)
        modelBuilder.Entity<Artikal>()
            .HasOne(a => a.Korisnik)
            .WithMany() // or .WithMany(u => u.Artikli) if you add the collection property
            .OnDelete(DeleteBehavior.Cascade);

        // Narudzba → ApplicationUser (Korisnik)
        modelBuilder.Entity<Narudzba>()
            .HasOne(n => n.Korisnik)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // Narudzba → ApplicationUser (KurirskaSluzba)
        modelBuilder.Entity<Narudzba>()
            .HasOne(n => n.KurirskaSluzba)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict); // prevent accidental mass-deletion
        
        base.OnModelCreating(modelBuilder);
    }
}