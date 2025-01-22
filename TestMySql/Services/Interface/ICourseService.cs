using TestMySql.Entities;

namespace TestMySql.Services.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<IEnumerable<Course>> SortingByCourseNameAsync();
        Task<IEnumerable<Course>> PaginationAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> FilteringByCourseIdAsync(int courseIdStart, int courseIdEnd);
    }
}
