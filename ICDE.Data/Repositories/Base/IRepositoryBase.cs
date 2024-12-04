using System.Linq.Expressions;

namespace ICDE.Data.Repositories.Base;
public interface IRepositoryBase<T> where T : class
{
    Task<T?> Create(T entity);
    Task<bool> Delete(T entity);
    Task<T?> Get(int id);
    Task<List<T>> GetList(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetList();
    Task<T?> Update(T entity);
}