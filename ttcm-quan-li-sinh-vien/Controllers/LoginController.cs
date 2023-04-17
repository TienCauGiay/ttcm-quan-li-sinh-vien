using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ttcm_quan_li_sinh_vien.EF;

namespace ttcm_quan_li_sinh_vien.Controllers
{
    public class LoginController : Controller
    {
        QLSVDbContext _context = null;

        public LoginController()
        {
            _context = new QLSVDbContext();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == u.Username && x.Password == u.Password);
            if (user != null) 
            {
                Session["User"] = user;
                if (user.RoleID == "admin")
                    return RedirectToAction("Index", "Admin");
                if (user.RoleID == "teacher")
                    return RedirectToAction("Index", "Teacher");
                if (user.RoleID == "student")
                    return RedirectToAction("Index", "Student");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Forgot()
        {
            return View();
        }
    }
}