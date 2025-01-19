using Microsoft.AspNetCore.Mvc;
using TestMySql.Entities;

namespace TestMySql.Repositories.Interface;

public interface ICoursesRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
