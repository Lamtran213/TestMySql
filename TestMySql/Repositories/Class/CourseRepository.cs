using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMySql.Entities;
using TestMySql.Repositories.Interface;

namespace TestMySql.Repositories.Class
{
    public class CourseRepository : ICoursesRepository<Course>
    {
        private readonly StudentCourseDbContext _context;
        public CourseRepository(StudentCourseDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Course entity)
        {
            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Courses.FindAsync(id);
            if (entity != null)
            {
                _context.Courses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task UpdateAsync(Course entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Course>> SortingByCourseNameAsync()
        {
            return await _context.Courses.OrderByDescending(c => c.CourseName).ToListAsync();
        }
        public async Task<IEnumerable<Course>> PaginationAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<IEnumerable<Course>> FilteringByCourseIdAsync(int courseId, int courseIdEnd)
        {
            return await _context.Courses.Where(c => c.CourseId >= courseId && c.CourseId <= courseIdEnd).ToListAsync();
        }
    }
}
