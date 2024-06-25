using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services;

/*
    The IUnitOfWork will be used to coordinate the work 
    of multiple repositories. It will have a property for 
    each repository. It will also have a method that will 
    save all the changes made to the database.
*/
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    IProfileRepository ProfileRepository {get;}
    Task<bool> Complete();
    bool HasChanges();
}