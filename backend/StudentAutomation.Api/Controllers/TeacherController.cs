using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAutomation.Api.Data;
using StudentAutomation.Api.Models;

namespace StudentAutomation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeachersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      
        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddTeacher([FromBody] RegisterModel model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if(existingUser != null) return BadRequest("Username already exists");

            var teacher = new ApplicationUser
            {
                UserName = model.Username,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(teacher, model.Password);
            if(!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(teacher, "Teacher");
            return Ok("Teacher added successfully");
        }

  
        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateTeacher(string id, [FromBody] RegisterModel model)
        {
            var teacher = await _userManager.FindByIdAsync(id);
            if(teacher == null) return NotFound();

            teacher.FullName = model.FullName;
            if(!string.IsNullOrEmpty(model.Password))
                await _userManager.RemovePasswordAsync(teacher)
                      .ContinueWith(async t => await _userManager.AddPasswordAsync(teacher, model.Password));

            await _context.SaveChangesAsync();
            return Ok("Teacher updated successfully");
        }

   
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteTeacher(string id)
        {
            var teacher = await _userManager.FindByIdAsync(id);
            if(teacher == null) return NotFound();

            await _userManager.DeleteAsync(teacher);
            return Ok("Teacher deleted successfully");
        }

        
        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetTeachers()
        {
            var users = await _context.Users.ToListAsync();
            var teachers = new List<object>();
            foreach(var user in users)
            {
                if(await _userManager.IsInRoleAsync(user, "Teacher"))
                    teachers.Add(new { user.Id, user.UserName, user.FullName });
            }
            return Ok(teachers);
        }
    }
}
