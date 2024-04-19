namespace API.Services.IRepositories;

/*
    The IUnitOfWork will be used to coordinate the work 
    of multiple repositories. It will have a property for 
    each repository. It will also have a method that will 
    save all the changes made to the database.
*/

public interface IUnitOfWork
{
    IPlayerRepository Players { get; } // we have only get because we don't want to set the repository. setting the repository will be done in the UnitOfWork class

    Task CompleteAsync(); // this method will save all the changes made to the database
}
