using System.Collections;
using Core.Data;
using Core.Services;
using Core.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UnitOfWork(DataContext context) : IUnitOfWork
{
    private readonly DataContext _context = context;
    private Hashtable _repositories;

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        _repositories ??= [];

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>) _repositories[type];
    }
}