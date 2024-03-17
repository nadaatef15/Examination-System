using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class CourseRepo : CourseIRepo
    {
        ExaminationContext db;
        public CourseRepo(ExaminationContext _db)
        {
            db= _db;
        } 
        public List<Course> getAll()
        {
            return db.Courses.FromSqlRaw("EXECUTE dbo.GetCourseData").ToList();
        }
    }
}
