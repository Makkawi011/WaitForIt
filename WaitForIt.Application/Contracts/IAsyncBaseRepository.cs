using System;
namespace WaitForIt.Application.Contracts;

public interface IAyncEntityRepository<T> 
    where T : class
{
    //Commands
    Task<Guid> Add(T entity);
    Task<(bool IsDelete ,string Msg)> Remove(Guid id);
    Task<(bool IsUpdate ,string Msg)> Update(T entity);

    //Queries
    Task<T> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
}
