using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IAuthRepo
    {
        Admin FindAdmin(string Email,string Password);

        Instructor FindInstructor(string Email, string Password);

        Student FindStudent(string Email, string Password);
    }
}
