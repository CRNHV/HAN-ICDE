using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
public class OpdrachtBeoordelingRepository : CrudRepositoryBase<OpdrachtBeoordeling>, IOpdrachtBeoordelingRepository
{
    private readonly AppDbContext _context;

    public OpdrachtBeoordelingRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<OpdrachtBeoordeling>> HaalBeoordelingenOpVoorStudent(int userId)
    {
        return await _context.Studenten
    .Include(s => s.IngeleverdeOpdrachten)
        .ThenInclude(io => io.Beoordelingen)
            .ThenInclude(b => b.IngeleverdeOpdracht)
                .ThenInclude(io => io.Opdracht)
    .Where(s => s.User.Id == userId)
    .SelectMany(s => s.IngeleverdeOpdrachten)
    .SelectMany(io => io.Beoordelingen)
    .GroupBy(b => b.IngeleverdeOpdracht.Opdracht) // Group by Opdracht
    .Select(g => g.OrderByDescending(b => b.Cijfer).FirstOrDefault()) // Pick the highest Cijfer in each group
    .ToListAsync();
    }
}
