using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories.Base;
public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly AppDbContext _context;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T?> Create(T entity)
    {
        await _context.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public virtual async Task<T?> Get(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> Update(T entity)
    {
        _context.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public virtual async Task<bool> Delete(T entity)
    {
        _context.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> predicate)
    {
        var result = await _context.Set<T>().Where(predicate).ToListAsync();
        return result;
    }

    public virtual async Task<List<T>> GetList()
    {
        return await _context.Set<T>().ToListAsync();
    }
}
