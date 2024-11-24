using System.Linq.Expressions;

namespace ICDE.Data.Repositories;
public interface IRepositoryBase<T> where T : class
{
    Task<T?> Create(T entity);
    Task<bool> Delete(T entity);
    Task<T?> Get(int id);
    Task GetList(Expression<Func<T, bool>> predicate);
    Task<T?> Update(T entity);
}