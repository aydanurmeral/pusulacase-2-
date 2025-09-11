using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAutomation.Api.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

 
        [Required]
        public string UserId { get; set; } = string.Empty;

   
        [Required]
        public string Grade { get; set; } = string.Empty;


        public int Absences { get; set; } = 0;
    }
}
