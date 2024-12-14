using System.Linq.Expressions;
using ICDE.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories.Base;

public interface IVersionableRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> AlleUnieke();
    Task<TEntity?> Versie(Guid groupId, int versieNummer);
    Task<TEntity?> NieuwsteVoorGroepId(Guid groupId);
    Task<List<TEntity>> EerdereVersies(Guid groupId, int versieNummer);
}

public interface ICrudRepository<TEntity> where TEntity : class
{
    Task<TEntity?> Maak(TEntity request);
    Task<TEntity?> VoorId(int id);
    Task<bool> Verwijder(TEntity dbEntity);
    Task<bool> Update(TEntity dbEntity);
    Task<List<TEntity>> Lijst(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> Lijst();
}

public abstract class CrudRepositoryBase<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public CrudRepositoryBase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TEntity>> Lijst(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _context.Set<TEntity>().Where(predicate).ToListAsync();
        return result;
    }

    public async Task<List<TEntity>> Lijst()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> Maak(TEntity entity)
    {
        await _context.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public async Task<bool> Update(TEntity dbEntity)
    {
        _context.Update(dbEntity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Verwijder(TEntity dbEntity)
    {
        _context.Remove(dbEntity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<TEntity?> VoorId(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
}

public abstract class VersionableRepositoryBase<TEntity> : CrudRepositoryBase<TEntity>, IVersionableRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    protected VersionableRepositoryBase(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<TEntity>> AlleUnieke()
    {
        return await _context.Set<TEntity>()
            .GroupBy(x => ((IVersionable)x).GroupId)
            .Select(g => g.OrderByDescending(l => ((IVersionable)l).VersieNummer).First())
            .ToListAsync();
    }

    public async Task<List<TEntity>> EerdereVersies(Guid groupId, int versieNummer)
    {
        return await _context.Set<TEntity>()
            .Where(x => ((IVersionable)x).GroupId == groupId && ((IVersionable)x).VersieNummer != versieNummer)
            .ToListAsync();
    }

    public abstract Task<TEntity?> Versie(Guid groupId, int versieNummer);

    public abstract Task<TEntity?> NieuwsteVoorGroepId(Guid groupId);
}