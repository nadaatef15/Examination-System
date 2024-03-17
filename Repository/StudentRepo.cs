using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class StudentRepo : StudentIRepo
    {
        ExaminationContext db;

        public StudentRepo(ExaminationContext _db)
        {
            db = _db;
        }
        public void Add(Student student)
        {
            db.Database.ExecuteSqlRaw("EXECUTE dbo.AddStudent {0}, {1}, {2}, {3}, {4}, {5}",
                                       student.StudentFname,
                                       student.StudentLname,
                                       student.StudentEmail,
                                       student.StudentGender,
                                       student.StudentPassword,
                                       student.TrackId);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            // Delete all related records in StudentCourse table first
            var studentCourses = db.StudentCourses.Where(sc => sc.StudentId == id);
            db.StudentCourses.RemoveRange(studentCourses);
            db.SaveChanges();

            db.Database.ExecuteSqlRaw("EXECUTE dbo.DeleteStudent {0}", id);
            db.SaveChanges();
        }


        public void Edit(int id, Student student)
        {
            db.Database.ExecuteSqlRaw("EXECUTE dbo.UpdateStudent {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                                       id,
                                       student.StudentFname,
                                       student.StudentLname,
                                       student.StudentEmail,
                                       student.StudentGender,
                                       student.StudentPassword,
                                       student.TrackId);
            db.SaveChanges();
        }
        public bool EmailIsExist(string email)
        {
            return db.Students.Any(a => a.StudentEmail == email);
        }


        public List<Student> getAll()
        {
            return db.Students.FromSqlRaw("EXECUTE dbo.GetStudentData").ToList();
        }

        public Student getById(int id)
        {
            return db.Students.FromSqlRaw("EXECUTE dbo.GetStudentById {0}", id).AsEnumerable().FirstOrDefault();
        }

    }
}
