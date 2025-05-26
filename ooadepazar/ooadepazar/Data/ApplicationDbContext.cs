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
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        }
        
        modelBuilder.Entity<Artikal>().ToTable("Artikal");
        modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
        modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
        modelBuilder.Entity<Notifikacija>().ToTable("Notifikacija");
        modelBuilder.Entity<Pracenje>().ToTable("Pracenje");
        
        base.OnModelCreating(modelBuilder);
    }
} 