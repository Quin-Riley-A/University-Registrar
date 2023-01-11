using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using URegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace URegistrar.Controllers
{
  public class StudentsController: Controller
  {
    private readonly URegistrarContext _db;

    public StudentsController(URegistrarContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Student> model = _db.Students.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      _db.Students.Add(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
        .Include(student => student.Enrollments)
        .ThenInclude(join => join.Course)
        .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }
    public ActionResult Edit(int id)
    {
      Student thisStudent = _db.Students
        .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }
    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Students.Update(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
  }
}

