using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult ManageTeacher()
        {
            return View();
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