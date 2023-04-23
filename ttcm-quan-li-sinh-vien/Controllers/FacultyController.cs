using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ttcm_quan_li_sinh_vien.EF;

namespace ttcm_quan_li_sinh_vien.Controllers
{
    public class FacultyController : BaseController
    {
        QLSVDbContext _context = null;

        public FacultyController()
        {
            _context = new QLSVDbContext(); 
        }
        public ActionResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFaculty(FACULTY faculty)
        {
            if (ModelState.IsValid)
            {
                if (_context.FACULTies.FirstOrDefault(x => x.FacultyID == faculty.FacultyID) == null)
                {
                    _context.FACULTies.Add(faculty);
                    _context.SaveChanges();
                    ViewBag.ErrorFaculty = "";
                    return RedirectToAction("ManageFaculty");
                }
                else
                {
                    ViewBag.ErrorFaculty = "Mã khoa đã tồn tại";
                }
            }
            return View(faculty);
        }

        public ActionResult ManageFaculty()
        {
            var listFaculty = _context.FACULTies.ToList();
            return View(listFaculty);
        }

        public ActionResult UpdateFaculty(string id)
        {
            var faculty = _context.FACULTies.FirstOrDefault(x => x.FacultyID == id);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult UpdateFaculty(FACULTY faculty)
        {
            var res = _context.FACULTies.FirstOrDefault(x => x.FacultyID == faculty.FacultyID);
            if(res != null)
            {
                res.Name= faculty.Name;
                _context.FACULTies.AddOrUpdate(res);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageFaculty");
        }

        public ActionResult DeleteFaculty(string id)
        {
            var faculty = _context.FACULTies.FirstOrDefault(x => x.FacultyID == id);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult DeleteFaculty(FACULTY faculty)
        {
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.FacultyID == faculty.FacultyID);
            var classes = _context.CLASSes.FirstOrDefault(x => x.FacultyID == faculty.FacultyID);
            var res = _context.FACULTies.FirstOrDefault(x => x.FacultyID == faculty.FacultyID);
            if (teacher == null && classes == null)
            {
                _context.FACULTies.Remove(res);
                ViewBag.ErrorFaculty = "";
                _context.SaveChanges();
            }
            else
            {
                ViewBag.ErrorFaculty = "Bạn không thể xóa khoa này";
                return View(faculty);
            }
            return RedirectToAction("ManageFaculty");
        }
    }
}