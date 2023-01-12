using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using URegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace URegistrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly URegistrarContext _db;

    public CoursesController(URegistrarContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses
        .Include(course => course.Enrollments)
        .ThenInclude(join => join.Student)
        .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }   
    public ActionResult Delete(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      _db.Courses.Remove(thisCourse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Course thisCourse = _db.Courses
        .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }
    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Courses.Update(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddStudent(int id)
    {
      Course thisCourse= _db.Courses.
        FirstOrDefault(course => course.CourseId == id);
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "StudentName");
      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult AddStudent(Course course, int studentId)
    {
      #nullable enable
      Enrollment? enrollment = _db.Enrollments.FirstOrDefault(join => (join.StudentId == studentId && join.CourseId == course.CourseId));
      #nullable disable
      if (enrollment == null && studentId != 0)
      {
        _db.Enrollments.Add(new Enrollment() {StudentId = studentId, CourseId = course.CourseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new {id = course.CourseId });
    }
    [HttpPost]
    public ActionResult DeleteStudent(int joinId)
    {
      Enrollment joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentId == joinId);
      _db.Enrollments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
