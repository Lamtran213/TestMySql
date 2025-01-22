using Microsoft.AspNetCore.Mvc;
using TestMySql.Entities;

namespace TestMySql.Repositories.Interface;

public interface ICoursesRepository<T> where T : class
{
    //Add more base repository methods here
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<T>> SortingByCourseNameAsync();
    Task<IEnumerable<T>> PaginationAsync(int pageNumber, int pageSize);
    Task<IEnumerable<T>> FilteringByCourseIdAsync(int courseIdStart, int courseIdEnd);
}
