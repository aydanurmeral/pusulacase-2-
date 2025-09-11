using Microsoft.AspNetCore.Identity;

namespace StudentAutomation.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public int? Grade { get; set; }

    }
}
