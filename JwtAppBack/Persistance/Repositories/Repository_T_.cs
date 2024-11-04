using System.Linq.Expressions;
using System.Net.Http.Headers;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Core.Domain;
using JwtAppBack.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace JwtAppBack.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly JwtAppContext _context;

    public Repository(JwtAppContext context)
    {
        _context = context;
    }


    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync(); 
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
    }

    public Task<T?> GetByIdAsync(object id)
    {
        return _context.Set<T>().FirstOrDefaultAsync(x=>x.Id == (int)id);
    }

    public async Task RemoveAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity, T unchanged)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

}
