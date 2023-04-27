using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
        public ActionResult UpdateTeacher(TEACHER teacher, HttpPostedFileBase file)
        {
            var res = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
            res.FacultyID = teacher.FacultyID;
            res.FullName = teacher.FullName;
            res.Birthday = teacher.Birthday;
            res.Gender = teacher.Gender;
            res.Address = teacher.Address;
            res.PhoneNumber = teacher.PhoneNumber;
            res.Email = teacher.Email;
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                         || Path.GetExtension(file.FileName).ToLower() == ".png"
                        || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/assets/img/teacher"), file.FileName);
                        file.SaveAs(path);
                        res.Image = file.FileName;
                    }
                }
            }
            _context.TEACHERs.AddOrUpdate(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Cập nhật thông tin thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Student(int? page)
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.CLASS.FacultyID == teacher.FacultyID).ToList();
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.FacultyID == teacher.FacultyID).DistinctBy(c => c.Name).ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchStudent(int? page, string searchStudent, string classlist)
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.ClassID.Contains(classlist) && x.FullName.Contains(searchStudent)).ToList();
            ViewBag.SearchStudent = searchStudent;
            ViewBag.Classes = classlist;
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.FacultyID == teacher.FacultyID).DistinctBy(c => c.Name).ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Score()
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.FacultyID == teacher.FacultyID).DistinctBy(c => c.Name).ToList();
            ViewBag.SubjectList = _context.SUBJECTs.ToList();
            return View();
        }

        public ActionResult ScoreStudentByClass(int? page, string classlist, string subjectlist)
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listScoreStudent = _context.SCOREs.Where(x => x.STUDENT.ClassID.Contains(classlist) && x.SubjectID.Contains(subjectlist)).ToList();
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.FacultyID == teacher.FacultyID).DistinctBy(c => c.Name).ToList();
            ViewBag.SubjectList = _context.SUBJECTs.ToList();
            ViewBag.ClassListSearch = classlist;
            ViewBag.SubjectListSearch = subjectlist;
            return View(listScoreStudent.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult InputScore(HttpPostedFileBase excelFile)
        {
            if (excelFile == null || excelFile.ContentLength == 0)
            {
                TempData["AlertMessage"] = "Vui lòng chọn một file Excel";
                return RedirectToAction("Score");
            }

            if (!excelFile.FileName.EndsWith(".xlsx") && !excelFile.FileName.EndsWith(".xls"))
            {
                TempData["AlertMessage"] = "File không đúng định dạng";
                return RedirectToAction("Score");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Đọc dữ liệu từ file Excel và lưu vào cơ sở dữ liệu
            using (var package = new ExcelPackage(excelFile.InputStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet != null)
                {
                    try
                    {
                        int rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            string studentId = worksheet.Cells[row, 1].Value?.ToString();
                            string subjectId = worksheet.Cells[row, 2].Value?.ToString();
                            double diemcc = double.Parse(worksheet.Cells[row, 3].Value?.ToString());
                            double diemkt = double.Parse(worksheet.Cells[row, 4].Value?.ToString());
                            double diemThi = double.Parse(worksheet.Cells[row, 5].Value?.ToString());
                            double diemTB = double.Parse(worksheet.Cells[row, 6].Value?.ToString());

                            // Lưu điểm của sinh viên vào cơ sở dữ liệu
                            var score = new SCORE
                            {
                                StudentID = studentId,
                                SubjectID = subjectId,
                                ScoreCC = diemcc,
                                ScoreKT = diemkt,
                                DiemThi = diemThi,
                                DiemTB = diemTB
                            };
                            _context.SCOREs.Add(score);
                            _context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["AlertMessage"] = "Nhập điểm sinh viên không thành công, vui lòng kiểm tra lại file excel";
                        return RedirectToAction("Score");
                    }
                }
            }

            TempData["AlertMessage"] = "Nhập điểm sinh viên thành công";
            return RedirectToAction("Score");
        }

        public ActionResult UpdateScore(string studentid, string subjectid)
        {
            var score = _context.SCOREs.FirstOrDefault(x => x.StudentID == studentid && x.SubjectID == subjectid);
            return View(score);
        }

        [HttpPost]
        public ActionResult UpdateScore(SCORE score)
        {
            var res = _context.SCOREs.FirstOrDefault(x => x.StudentID == score.StudentID && x.SubjectID == score.SubjectID);
            res.ScoreCC = score.ScoreCC;
            res.ScoreKT = score.ScoreKT;
            res.DiemThi = score.DiemThi;
            res.DiemTB = (score.ScoreCC + score.ScoreKT * 2 + score.DiemThi * 3) / 6;
            _context.SaveChanges();
            return RedirectToAction("Score");
        }

        public ActionResult Schedule()
        {
            var user = (User)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.TeacherID == user.Username);
            var res = _context.REGISTERSUBJECTs.Where(x => x.TeacherName.Contains(teacher.FullName)).DistinctBy(x => x.TimeLearning).ToList();
            return View(res);
        }

        public ActionResult ChangePassword()
        {
            var user = (User)Session["User"];
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            var res = _context.Users.FirstOrDefault(x => x.Username == user.Username);
            res.Password = user.Password;
            _context.SaveChanges();
            TempData["AlertMessage"] = "Bạn đã đổi mật khẩu thành công";
            return RedirectToAction("Index");
        }
    }
}