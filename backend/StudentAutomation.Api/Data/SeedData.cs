using StudentAutomation.Api.Models;

namespace StudentAutomation.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // --- Teachers ---
            if (!context.Teachers.Any())
            {
                context.Teachers.AddRange(
                    new Teacher { Name = "Ahmet Kaya", UserId = "teacher1-id" },
                    new Teacher { Name = "Ayşe Yıldız", UserId = "teacher2-id" }
                );
                context.SaveChanges();
            }

            // --- Students ---
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student { Name = "Mehmet Yılmaz", Grade = "9A", UserId = "student1-id" },
                    new Student { Name = "Elif Demir", Grade = "10B", UserId = "student2-id" }
                );
                context.SaveChanges();
            }

            // --- Courses ---
            if (!context.Courses.Any())
            {
                var teacher1 = context.Teachers.First(t => t.Name == "Ahmet Kaya");
                var teacher2 = context.Teachers.First(t => t.Name == "Ayşe Yıldız");

                context.Courses.AddRange(
                    new Course { Title = "Matematik", Status = "Aktif", TeacherId = teacher1.Id },
                    new Course { Title = "Fen Bilgisi", Status = "Pasif", TeacherId = teacher2.Id }
                );
                context.SaveChanges();
            }
        }
    }
}
