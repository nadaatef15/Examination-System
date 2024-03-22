using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Exam_System.Repository
{
    public class AuthRepo :IAuthRepo
    {

        ExaminationContext db;
        public AuthRepo(ExaminationContext _db)
        {
            db = _db;
        }

        public Admin FindAdmin(string _Email, string _Password)
        {
            return db.Admins.SingleOrDefault(x => x.Email == _Email && x.Password == _Password);
        }

        public Instructor FindInstructor(string _Email, string _Password)
        {
            return db.Instructors.SingleOrDefault(x => x.InstructorEmail == _Email && x.InstructorPassword == _Password);
        }

        public Student FindStudent(string _Email, string _Password)
        {
            return db.Students.SingleOrDefault(x => x.StudentEmail == _Email && x.StudentPassword == _Password);
        }
    }
}
