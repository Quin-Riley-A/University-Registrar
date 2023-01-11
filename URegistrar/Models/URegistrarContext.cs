using Microsoft.EntityFrameworkCore;

namespace URegistrar.Models
{
  public class URegistrarContext : DbContext
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public URegistrarContext (DbContextOptions options) : base (options) { }
  }
}