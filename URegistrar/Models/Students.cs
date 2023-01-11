using System.Collections.Generic;

namespace URegistrar.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string StartDate { get; set; }
    public Course Course { get; set; }
    public List<Enrollment> Enrollments { get; }
  }
}