namespace TestMySql.Services.Class;
using TestMySql.Entities;
using TestMySql.Exceptions;
using TestMySql.Repositories.Interface;
using TestMySql.Services.Interface;

public class CourseService : ICourseService
{
    private readonly ICoursesRepository<Course> _courseRepository;

    public CourseService(ICoursesRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        GlobalException.ThrowIfNull(courses, "No courses found.");
        return courses;
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        GlobalException.ThrowIfNotFound(course, $"Course with ID {id} not found.");
        return course;
    }

    public async Task AddCourseAsync(Course course) => await _courseRepository.AddAsync(course);

    public async Task UpdateCourseAsync(Course course)
    {
        GlobalException.ThrowIfNull(course, "Course cannot be null.");
        await _courseRepository.UpdateAsync(course);
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        GlobalException.ThrowIfNotFound(course, $"Course with ID {id} not found.");
        await _courseRepository.DeleteAsync(id);
    }
    public async Task<IEnumerable<Course>> SortingByCourseNameAsync()
    {
        var courses = await _courseRepository.SortingByCourseNameAsync();
        GlobalException.ThrowIfNull(courses, "No courses found.");
        return courses;
    }
    public async Task<IEnumerable<Course>> PaginationAsync(int pageNumber, int pageSize)
    {
        var courses = await _courseRepository.PaginationAsync(pageNumber, pageSize);
        GlobalException.ThrowIfNull(courses, "No courses found.");
        return courses;
    }
    public async Task<IEnumerable<Course>> FilteringByCourseIdAsync(int courseIdStart, int courseIdEnd)
    {
        var courses = await _courseRepository.FilteringByCourseIdAsync(courseIdStart, courseIdEnd);
        GlobalException.ThrowIfNull(courses, "No courses found.");
        return courses;
    }
}
