using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Exam_System.Repository
{
    public class StudentCourseRepo : StudentCourseIRepo
    {
        private readonly ExaminationContext _db;

        public StudentCourseRepo(ExaminationContext db)
        {
            _db = db;
        }

        public void Add(StudentCourse studentCourse)
        {
            _db.Database.ExecuteSqlRaw("EXECUTE dbo.AddStudentCourse {0}, {1}, {2}",
                                       studentCourse.StudentId,
                                       studentCourse.CourseId,
                                       studentCourse.Grade);
        }

        public void Delete(int courseId, int studentId)
        {
           

            _db.Database.ExecuteSqlRaw("EXECUTE dbo.DeleteStudentCourse {0}, {1}",
                                       studentId, courseId);
        }

        public List<StudentCourse> getAll()
        {
            return _db.StudentCourses.FromSqlRaw("EXECUTE dbo.GetStudentCourseData").ToList();
        }

        public List<StudentCourse> getByCourseId(int courseId)
        {
            return _db.StudentCourses.FromSqlRaw("EXECUTE dbo.GetStudentCourseDataByCourseId {0}", courseId).ToList();
        }

        public StudentCourse getByIds(int courseId, int studentId)
        {
            return _db.StudentCourses.FromSqlRaw("EXECUTE dbo.GetStudentCourseDataById {0}, {1}", courseId, studentId).AsEnumerable().FirstOrDefault();
        }

        public List<StudentCourse> getByStudentId(int studentId)
        {
            //return _db.StudentCourses.FromSqlRaw("EXECUTE dbo.GetStudentCourseDataByStudentId {0}", studentId).ToList();
            return _db.StudentCourses.Where(a=>a.StudentId == studentId).ToList();
        }

        public void UpdateGrade(StudentCourse obj, int grade)
        {
            _db.Database.ExecuteSqlRaw("EXECUTE dbo.UpdateStudentCourse {0}, {1}, {2}",
                                       obj.StudentId,
                                       obj.CourseId,
                                       grade);
        }
    }
}
