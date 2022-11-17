using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Anasayfa(short Role, string? StudentId)
        {
            ViewBag.Role = Role;
            List<Student> StudentList = new List<Student>();
            if (Role == 0)//Student
            {
                var student = _context.Students.FirstOrDefault(x => x.StudentId == Convert.ToInt32(StudentId));
                if (student != null)
                {
                    StudentList.Add(student);
                }

            }
            else//Admin
            {


                var Students = _context.Students.ToList();
                foreach (var student in Students)
                {

                    StudentList.Add(new Student
                    {
                        StudentId = student.StudentId,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        BirthDate = student.BirthDate,
                        CourseId = student.CourseId
                    });
                }
                //var CourseList = _context.Courses.ToList();
            }

            return View(StudentList);
        }
        public JsonResult LoginCheck(string username, string password)
        {
            string sonuc = "";
            if (_context.Users.Any(x => x.UserName == username))
            {
                var user = _context.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    var role = user.Role;
                    return Json(new
                    {
                        isRedirect = true,
                        url = "/Home/Anasayfa",
                        role = role,
                        stuId = user.StudentId.ToString(),
                    });
                }
                else
                {
                    sonuc = "Şifre Hatalı!";
                    return Json(sonuc);

                }
            }
            else
            {
                sonuc = "Kullanıcı bulunamadı!";
                return Json(sonuc);

            }
        }
    
        public JsonResult AddStudent(string FirstName, string LastName, string BirthDate, string Courses)
        {

            Student student = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = DateTime.Parse(BirthDate),
                CourseId = Courses
            };



            _context.Students.Add(student);

            _context.SaveChanges();


            //Bu kısmı sonradan düşündüm ve yapıyı bozmamak için bu şekilde yazdım
            var StuId = _context.Students.OrderByDescending(x => x.StudentId).FirstOrDefault();
            User user = new User()
            {
                UserName = FirstName + LastName,
                StudentId = StuId.StudentId,
                Password = "StuPass123",
                Role = 0
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Json(new
            {
                status = "Öğrenci Başarıyla Eklendi"
            });
        }

        public JsonResult UpdateStudent(string FirstName, string LastName, string BirthDate, string Courses, string StudentId)
        {
            var student = _context.Students.FirstOrDefault(x => x.StudentId == Convert.ToInt32(StudentId));

            student.FirstName = FirstName;
            student.LastName = LastName;
            student.BirthDate = DateTime.Parse(BirthDate);
            student.CourseId = Courses;


            //_context.Students.Add(student);
            _context.SaveChanges();

            return Json(new
            {
                status = "Öğrenci Başarıyla Güncellendi"
            });
        }

        public JsonResult DeleteStudent(string StudentId)
        {
            var student = _context.Students.FirstOrDefault(x => x.StudentId == Convert.ToInt32(StudentId));

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Json(new
            {
                status = "Öğrenci Başarıyla Silindi"
            });
        }
        public JsonResult StudentDetails(string StudentId)
        {
            var student = _context.Students.FirstOrDefault(x => x.StudentId == Convert.ToInt32(StudentId));

            return Json(new
            {
                status = student
            });
        }

        #region Zaman kalmadığı için yapılamadı
        public IActionResult ChangePassword()
        {
            return View();
        }
        public JsonResult AddCourses()
        {
            return Json(new
            {
                CourseId = "Yeni Ders Kodu"
            });
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult UpdateUser()
        {
            return View();
        }
        public void DeleteUser()
        {

        }

        #endregion
    }
}
