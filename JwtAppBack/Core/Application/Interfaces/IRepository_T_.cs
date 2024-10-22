using System.Linq.Expressions;
using JwtAppBack.Core.Domain;

namespace JwtAppBack.Core.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T? > GetByIdAsync(object id);
    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);

    Task CreateAsync(T entity);
    Task UpdateAsync(T entity, T unchanged);
    Task RemoveAsync(T entity);

}
