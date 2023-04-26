using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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

        public ActionResult Score(int? page)
        {
            var user = (User)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.StudentID == user.Username);
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listScore = _context.SCOREs.Where(x => x.StudentID == student.StudentID).ToList();
            ViewBag.Semester = _context.SEMESTERs.ToList();
            return View(listScore.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ScoreBySemester(string semester)
        {
            var user = (User)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.StudentID == user.Username);
            var listScore = _context.SCOREs.Where(x => x.SUBJECT.SemesterID == semester && x.StudentID == student.StudentID).ToList();
            ViewBag.Semester = _context.SEMESTERs.ToList();
            return View(listScore);
        }

        public ActionResult Register()
        {
            var user = (User)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.StudentID == user.Username);
            var listSubject = (from s in _context.SUBJECTs
                              where !(from sc in _context.SCOREs where sc.StudentID == student.StudentID select sc.SubjectID).Contains(s.SubjectID)
                              select s).ToList();
            ViewBag.Subjects = new SelectList(listSubject, "SubjectID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Register(REGISTERSUBJECT register)
        {
            var user = (User)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.StudentID == user.Username);
            var subject = _context.SUBJECTs.FirstOrDefault(x => x.SubjectID == register.SubjectName);
            var idRegsiter = student.StudentID + "_" + subject.SubjectID;
            REGISTERSUBJECT r = new REGISTERSUBJECT();
            r.RegisterSubjectID = idRegsiter;
            r.StudentID= student.StudentID;
            r.SubjectName = subject.Name;
            _context.REGISTERSUBJECTs.Add(r);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Bạn đã đăng kí học môn " + subject.Name + " thành công";
            return RedirectToAction("Schedule");
        }

        public ActionResult Schedule()
        {
            return View();
        }
    }
}