using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAutomation.Api.Data;
using StudentAutomation.Api.Models;

namespace StudentAutomation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _context.Courses
                .Include(c => c.Teacher)   
                .Include(c => c.Students)  
                .ToListAsync();

            return Ok(courses);
        }

  
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }
    }
}
