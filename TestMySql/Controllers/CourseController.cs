using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestMySql.DTO.request;
using TestMySql.Entities;
using TestMySql.Services.Interface;

namespace TestMySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper mapper;

        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(mapper.Map<IEnumerable<Course>>(courses));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Course>(course));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<Course>> PostCourse(CourseCreationRequest courseRequest)
        {
            var course = mapper.Map<Course>(courseRequest);

            await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
        
        [HttpGet("sorting")]
        public async Task<ActionResult<IEnumerable<Course>>> SortingByCourseName()
        {
            var courses = await _courseService.SortingByCourseNameAsync();
            return Ok(courses);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<Course>>> Pagination(int pageNumber, int pageSize)
        {
            var courses = await _courseService.PaginationAsync(pageNumber, pageSize);
            return Ok(courses);
        }

        [HttpGet("filtering")]
        public async Task<ActionResult<IEnumerable<Course>>> FilteringByCourseId(int courseIdStart, int courseIdEnd)
        {
            var courses = await _courseService.FilteringByCourseIdAsync(courseIdStart, courseIdEnd);
            return Ok(courses);
        }
    }

}
