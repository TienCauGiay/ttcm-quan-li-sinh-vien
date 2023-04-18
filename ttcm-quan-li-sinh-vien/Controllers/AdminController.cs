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
    public class AdminController : BaseController
    {
        QLSVDbContext _context = null;

        public AdminController()
        {
            _context = new QLSVDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTeacher()
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddTeacher(TEACHER teacher) 
        {
            if(ModelState.IsValid)
            {
                if (_context.TEACHERs.FirstOrDefault(x => x.TeacherID == teacher.TeacherID) == null)
                {
                    _context.TEACHERs.Add(teacher);
                    _context.SaveChanges();
                    ViewBag.ErrorTeacher = "";
                    return RedirectToAction("ManageTeacher");
                }
                else
                {
                    ViewBag.ErrorTeacher = "Mã giảng viên đã tồn tại";
                }
            }
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            return View(teacher);
        }

        public ActionResult ManageTeacher(int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listTeacher = _context.TEACHERs.ToList();   
            return View(listTeacher.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchTeacher(int? page, string searchTeacher)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listTeacher = _context.TEACHERs.Where(x => x.FullName.Contains(searchTeacher)).ToList();
            ViewBag.SearchTeacher = searchTeacher;
            return View(listTeacher.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateTeacher(string id)
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID== id);
            return View(res);
        }

        [HttpPost]
        public ActionResult UpdateTeacher(TEACHER teacher)
        {
            if(ModelState.IsValid)
            {
                var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
                res.FacultyID= teacher.FacultyID;
                res.FullName= teacher.FullName;
                res.Birthday= teacher.Birthday;
                res.Gender= teacher.Gender;
                res.Address= teacher.Address;   
                res.PhoneNumber = teacher.PhoneNumber;
                res.Email = teacher.Email;
                res.Image = teacher.Image;
                _context.TEACHERs.AddOrUpdate(res);
                _context.SaveChanges();
                return RedirectToAction("ManageTeacher");
            }
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            return View(teacher);
        }

        public ActionResult DeleteTeacher(string id)
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult DeleteTeacher(TEACHER teacher)
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "FacultyID", "Name");
            var teacherClass = _context.CLASSes.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
            if(teacherClass != null)
            {
                ViewBag.ErrorTeacher = "Không thể xóa giảng viên này";
                return View(teacher);
            }
            else
            {
                var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
                ViewBag.ErrorTeacher = "";
                _context.TEACHERs.Remove(res);
                _context.SaveChanges();
                return RedirectToAction("ManageTeacher");
            }
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        public ActionResult ManageStudent()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult ManageUser()
        {
            return View();
        }
    }
}