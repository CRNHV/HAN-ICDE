using ICDE.Data.Entities.Identity;
using ICDE.Data.Entities.Opdracht;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data;
public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Opdracht> Opdrachten { get; set; }
    public DbSet<IngeleverdeOpdracht> IngeleverdeOpdrachten { get; set; }
    public DbSet<OpdrachtBeoordeling> OpdrachtBeoordelingen { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
