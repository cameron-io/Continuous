using Core.Data;
using Core.Repositories;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Context;
using Infrastructure.Specifications;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(DataContext context)
    : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _context = context;

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    
    public virtual void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Upsert(T entity)
    {
        _context.Entry(entity).State = entity.Id == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
    }
    
    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public virtual async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
}