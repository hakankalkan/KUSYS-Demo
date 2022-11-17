using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
    public class MyContext:DbContext
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { 
        
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
