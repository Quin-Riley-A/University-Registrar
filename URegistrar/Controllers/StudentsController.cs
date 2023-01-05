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
  }
}