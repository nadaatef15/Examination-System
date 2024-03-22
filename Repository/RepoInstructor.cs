using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class RepoInstructor : IRepoInstructor
    {
        ExaminationContext db;
        public RepoInstructor(ExaminationContext _db)
        {
            db = _db;
        }
        public List<Course> getCourseForInstructor(int id)
        {
            return db.Instructors.Where(a => a.InstructorId == id).SelectMany(a => a.Courses).ToList();
        }


       
    }
}
