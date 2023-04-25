using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ttcm_quan_li_sinh_vien.EF;

namespace ttcm_quan_li_sinh_vien.Controllers
{
    public class StudentController : BaseController
    {
        QLSVDbContext _context = null;
        public StudentController()
        {
            _context = new QLSVDbContext();
        }
        public ActionResult Index()
        {
            var user = (User)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.StudentID == user.Username);
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            return View(student);
        }

        public ActionResult UpdateStudent(STUDENT student) 
        {
            var res = _context.STUDENTs.FirstOrDefault(x => x.StudentID == student.StudentID);
            res.ClassID = student.ClassID;
            res.FullName = student.FullName;
            res.Birthday = student.Birthday;
            res.Gender = student.Gender;
            res.Address = student.Address;
            res.PhoneNumber = student.PhoneNumber;
            res.Email = student.Email;
            res.Image = student.Image;
            res.YearAdmission= student.YearAdmission;
            _context.STUDENTs.AddOrUpdate(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Cập nhật thông tin thành công";
            return RedirectToAction("Index");
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