using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Models;

namespace ooadepazar.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Artikal> Artikal { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artikal>().ToTable("Artikal");
        
        base.OnModelCreating(modelBuilder);
    }

public DbSet<ooadepazar.Models.Korisnik> Korisnik { get; set; } = default!;

public DbSet<ooadepazar.Models.Narudzba> Narudzba { get; set; } = default!;

public DbSet<ooadepazar.Models.Notifikacija> Notifikacija { get; set; } = default!;

public DbSet<ooadepazar.Models.Pracenje> Pracenje { get; set; } = default!;
} 