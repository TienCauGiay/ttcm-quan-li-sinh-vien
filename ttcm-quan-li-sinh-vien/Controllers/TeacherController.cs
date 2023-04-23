using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ttcm_quan_li_sinh_vien.EF;

namespace ttcm_quan_li_sinh_vien.Controllers
{
    public class TeacherController : BaseController
    {
        QLSVDbContext _context = null;

        public TeacherController() 
        {
            _context = new QLSVDbContext();
        }
        // GET: Teacher
        public ActionResult Index()
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            return View(teacher);
        }

        [HttpPost]
        public ActionResult UpdateTeacher(TEACHER teacher)
        {
            var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
            res.FacultyID = teacher.FacultyID;
            res.FullName = teacher.FullName;
            res.Birthday = teacher.Birthday;
            res.Gender = teacher.Gender;
            res.Address = teacher.Address;
            res.PhoneNumber = teacher.PhoneNumber;
            res.Email = teacher.Email;
            res.Image = teacher.Image;
            _context.TEACHERs.AddOrUpdate(res);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Student(int? page)
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.CLASS.FacultyID == teacher.FacultyID).ToList();
            ViewBag.ClassList = _context.CLASSes.DistinctBy(c => c.Name).ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Score()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }
    }
}