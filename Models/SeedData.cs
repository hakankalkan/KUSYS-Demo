using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
    public static class SeedData
    {
        public static void Seed (IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            MyContext context = scope.ServiceProvider.GetRequiredService<MyContext>();

           

            context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.Users.AddRange(new User
                {
                    UserName = "admin",
                    Password = "123456",
                    Role = 1
                });
            }
            if (!context.Students.Any())
            {
                context.Students.AddRange(new Student
                {
                    FirstName ="Hakan",
                    LastName ="Kalkan",
                    BirthDate = DateTime.Parse("20.05.1992"),
                    CourseId = "CSI102-CSI101",
                });
            }
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(new Course
                {
                    CourseId = "CSI102",
                    CourseName = "Algorithms"
                });
                context.Courses.AddRange(new Course
                {
                    CourseId = "CSI101",
                    CourseName = "Introduction to Computer Science"
                });
                context.Courses.AddRange(new Course
                {
                    CourseId = "MAT101",
                    CourseName = "Calculus"
                });
                context.Courses.AddRange(new Course
                {
                    CourseId = "PHY101",
                    CourseName = "Physics"
                });
            }

            context.SaveChanges();
        }
    }
}
