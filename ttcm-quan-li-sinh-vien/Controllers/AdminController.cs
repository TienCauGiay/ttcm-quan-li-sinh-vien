using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
                    if(_context.Users.FirstOrDefault(x => x.Username == teacher.TeacherID) == null)
                    {
                        var user = new User();
                        user.Username = teacher.TeacherID;
                        user.Password = "1";
                        user.RoleID = "teacher";
                        user.Status = true;
                        _context.Users.Add(user);
                    }
                    _context.SaveChanges();
                    ViewBag.ErrorTeacher = "";
                    TempData["AlertMessage"] = "Thêm giảng viên thành công";
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
                TempData["AlertMessage"] = "Cập nhật thông tin giảng viên thành công";
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
            var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
            ViewBag.ErrorTeacher = "";
            var user = _context.Users.FirstOrDefault(x => x.Username == res.TeacherID);
            _context.Users.Remove(user);
            _context.TEACHERs.Remove(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Xóa giảng viên thành công";
            return RedirectToAction("ManageTeacher");
        }

        public ActionResult AddStudent()
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(STUDENT student)
        {
            if (ModelState.IsValid)
            {
                if (_context.STUDENTs.FirstOrDefault(x => x.StudentID == student.StudentID) == null)
                {
                    _context.STUDENTs.Add(student);
                    if (_context.Users.FirstOrDefault(x => x.Username == student.StudentID) == null)
                    {
                        var user = new User();
                        user.Username = student.StudentID;
                        user.Password = "1";
                        user.RoleID = "student";
                        user.Status = true;
                        _context.Users.Add(user);
                    }
                    _context.SaveChanges();
                    ViewBag.ErrorStudent = "";
                    TempData["AlertMessage"] = "Thêm sinh viên thành công";
                    return RedirectToAction("ManageStudent");
                }
                else
                {
                    ViewBag.ErrorStudent = "Mã sinh viên đã tồn tại";
                }
            }
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            return View(student);
        }

        public ActionResult ManageStudent(int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchStudent(int? page, string searchStudent)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.FullName.Contains(searchStudent)).ToList();
            ViewBag.SearchStudent = searchStudent;
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateStudent(string id)
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            var res = _context.STUDENTs.FirstOrDefault(x => x.StudentID == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult UpdateStudent(STUDENT student)
        {
            if (ModelState.IsValid)
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
                TempData["AlertMessage"] = "Cập nhật thông tin sinh viên thành công";
                return RedirectToAction("ManageStudent");
            }
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            return View(student);
        }

        public ActionResult DeleteStudent(string id)
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            var res = _context.STUDENTs.FirstOrDefault(x => x.StudentID == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult DeleteStudent(STUDENT student)
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "ClassID", "Name");
            var resgisterSubject = _context.REGISTERSUBJECTs.FirstOrDefault(x => x.StudentID == student.StudentID);
            if (resgisterSubject != null)
            {
                ViewBag.ErrorStudent = "Không thể xóa sinh viên này";
                return View(student);
            }
            else
            {
                var scoreStudent = _context.SCOREs.Where(x=>x.StudentID== student.StudentID);
                if(scoreStudent != null)
                {
                    _context.SCOREs.RemoveRange(scoreStudent);
                }
                var res = _context.STUDENTs.FirstOrDefault(x => x.StudentID == student.StudentID);
                ViewBag.ErrorStudent = "";
                _context.STUDENTs.Remove(res);
                var user = _context.Users.FirstOrDefault(x => x.Username == student.StudentID);
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Xóa sinh viên thành công";
                return RedirectToAction("ManageStudent");
            }
        }

        public ActionResult ManageUser(int? page)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listUser = _context.Users.ToList();
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchUser(int? page, string searchUser)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listUser = _context.Users.Where(x=>x.Username.Contains(searchUser)).ToList();
            ViewBag.SearchUser = searchUser;
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateUser(string id)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Username== id);
            ViewBag.ListRole = new SelectList(_context.Roles.ToList(), "RoleID", "Description");
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            var res = _context.Users.FirstOrDefault(x => x.Username== user.Username);
            res.Password= user.Password;
            res.RoleID= user.RoleID;
            res.Status=user.Status;
            _context.SaveChanges();
            TempData["AlertMessage"] = "Cập nhật tài khoản thành công";
            return RedirectToAction("ManageUser");
        }

        public ActionResult DeleteUser(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(User user)
        {
            var res = _context.Users.FirstOrDefault(x => x.Username == user.Username);
            _context.Users.Remove(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Xóa tài khoản thành công";
            return RedirectToAction("ManageUser");  
        }
    }
}