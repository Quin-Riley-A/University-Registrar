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
  }
}
