using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
public class OpdrachtRepository : IOpdrachtRepository
{
    private readonly AppDbContext _context;

    public OpdrachtRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Opdracht>> HaalAlleOp()
    {
        return await _context.Opdrachten.ToListAsync();
    }

    public async Task<Opdracht?> GetById(int opdrachtId)
    {
        return await _context.Opdrachten.FirstOrDefaultAsync(x => x.Id == opdrachtId);
    }

    public async Task MaakOpdracht(string naam, string beschrijving, bool isToets)
    {
        var opdracht = new Opdracht()
        {
            Naam = naam,
            Beschrijving = beschrijving,
            Type = isToets ? OpdrachtType.Toets : OpdrachtType.Casus
        };

        await _context.Opdrachten.AddAsync(opdracht);
        await _context.SaveChangesAsync();
    }

    public async Task<Opdracht?> GetLatestByGroupId(Guid groupId)
    {
        return await _context.Opdrachten
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public async Task<Opdracht?> GetByInzendingId(int inzendingId)
    {
        return await _context.IngeleverdeOpdrachten
            .Where(x => x.Id == inzendingId)
            .Include(x => x.Opdracht)
            .ThenInclude(x => x.BeoordelingCritereas)
            .ThenInclude(x => x.Leeruitkomsten)
            .Select(x => x.Opdracht)
            .FirstOrDefaultAsync();

    }
}