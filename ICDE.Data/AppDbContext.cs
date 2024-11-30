using ICDE.Data.Entities;
using ICDE.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data;
public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Opdracht> Opdrachten { get; set; }
    public DbSet<IngeleverdeOpdracht> IngeleverdeOpdrachten { get; set; }
    public DbSet<OpdrachtBeoordeling> OpdrachtBeoordelingen { get; set; }
    public DbSet<Leeruitkomst> leeruitkomstsen { get; set; }
    public DbSet<Vak> Vakken { get; set; }
    public DbSet<Les> Lessen { get; set; }
    public DbSet<Opleiding> Opleidingen { get; set; }
    public DbSet<Cursus> Cursussen { get; set; }
    public DbSet<BeoordelingCriterea> BeoordelingCritereas { get; set; }
    public DbSet<Planning> Plannings { get; set; }
    public DbSet<PlanningItem> PlanningItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Opleiding>()
            .HasMany(e => e.Vakken)
            .WithOne();

        modelBuilder.Entity<Vak>()
           .HasMany(e => e.Cursussen)
           .WithMany()
           .UsingEntity(j => j.ToTable("VakCursussen"));

        modelBuilder.Entity<Vak>()
          .HasMany(e => e.Leeruitkomsten)
          .WithMany()
           .UsingEntity(j => j.ToTable("VakLeeruitkomsten"));

        modelBuilder.Entity<Les>()
            .HasMany(e => e.Leeruitkomsten)
            .WithMany()
            .UsingEntity(j => j.ToTable("LesLeeruitkomsten"));

        modelBuilder.Entity<Planning>()
         .HasMany(p => p.PlanningItems)
         .WithOne(pi => pi.Planning)
         .HasForeignKey(pi => pi.PlanningId);

        modelBuilder.Entity<PlanningItem>()
          .HasOne(pi => pi.Opdracht)
          .WithMany()
          .HasForeignKey(pi => pi.OpdrachtId);

        modelBuilder.Entity<PlanningItem>()
           .HasOne(pi => pi.Les)
           .WithMany()
           .HasForeignKey(pi => pi.LesId);

        modelBuilder.Entity<Opdracht>()
              .HasMany(o => o.BeoordelingCritereas)
              .WithOne()
              .HasForeignKey(bc => bc.OpdrachtId);

        modelBuilder.Entity<BeoordelingCriterea>()
            .HasMany(x => x.Leeruitkomsten)
            .WithMany();

        modelBuilder.Entity<IngeleverdeOpdracht>()
         .HasOne(io => io.Opdracht)
         .WithMany(o => o.IngeleverdeOpdrachten)
         .HasForeignKey(io => io.OpdrachtId);

        base.OnModelCreating(modelBuilder);
    }
}
