using Microsoft.EntityFrameworkCore;
using TestMySql.Entities;

namespace TestMySql.Repositories.Class;

public class EnrollmentRepository
{
    private readonly StudentCourseDbContext _context;
    public EnrollmentRepository(StudentCourseDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments.ToListAsync();
    }
}