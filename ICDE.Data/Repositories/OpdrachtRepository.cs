using ICDE.Data.Entities.Opdrachten;
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

    public Task<List<IngeleverdeOpdracht>> HaalInzendingenOp(int opdrachtId)
    {
        return _context.IngeleverdeOpdrachten.Where(x => x.OpdrachtId == opdrachtId).ToListAsync();
    }

    public async Task<Opdracht?> HaalOpdrachtOp(int opdrachtId)
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

    public async Task SlaIngeleverdeOpdrachtOp(IngeleverdeOpdracht ingeleverdeOpdracht)
    {
        await _context.IngeleverdeOpdrachten.AddAsync(ingeleverdeOpdracht);
        await _context.SaveChangesAsync();
    }

    public async Task SlaBeoordelingOp(OpdrachtBeoordeling beoordeling)
    {
        await _context.OpdrachtBeoordelingen.AddAsync(beoordeling);
        await _context.SaveChangesAsync();
    }

    public async Task<IngeleverdeOpdracht> HaalInzendingOp(int inzendingId)
    {
        return await _context.IngeleverdeOpdrachten.Where(x => x.Id == inzendingId).FirstOrDefaultAsync();
    }
}