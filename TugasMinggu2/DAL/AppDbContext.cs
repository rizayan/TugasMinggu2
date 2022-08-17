using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
