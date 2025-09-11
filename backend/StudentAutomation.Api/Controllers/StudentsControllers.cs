using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAutomation.Api.Data;
using StudentAutomation.Api.Models;

namespace StudentAutomation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

    
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Grade))
                return BadRequest(new { message = "İsim ve sınıf zorunludur" });

            var student = new Student
            {
                Name = dto.Name,
                Grade = dto.Grade,
                UserId = Guid.NewGuid().ToString(), 
                Absences = 0
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound(new { message = "Öğrenci bulunamadı" });

            student.Name = dto.Name;
            student.Grade = dto.Grade;

            await _context.SaveChangesAsync();
            return Ok(student);
        }

  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound(new { message = "Öğrenci bulunamadı" });

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Öğrenci silindi" });
        }
    }


    public class CreateStudentDto
    {
        public string Name { get; set; } = "";
        public string Grade { get; set; } = "";
    }

    public class UpdateStudentDto
    {
        public string Name { get; set; } = "";
        public string Grade { get; set; } = "";
    }
}
