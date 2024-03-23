using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace Exam_System.Repository
{
    public class InstructorCourseRepo : IInstructorCourseRepo
    {

        private readonly ExaminationContext db;
        readonly IExaminationContextProcedures dbProcedures;
        public InstructorCourseRepo(ExaminationContext _db, IExaminationContextProcedures _dbProcedures)
        {
            db = _db;
            dbProcedures = _dbProcedures;
        }
        public List<Course> getInstructorCoursesById(int instructorId)
        {
            return db.Courses.FromSqlRaw("EXECUTE dbo.GetAllInstructorCourses {0}", instructorId).ToList();
        }

        public async Task Add(int courseId , int instructorId)
        {
            await db.Database.ExecuteSqlRawAsync("EXEC AddInstructorCourse @InstructorId,@CourseId",
                     new SqlParameter("@InstructorId", instructorId),
                     new SqlParameter("@CourseId", courseId));

            /*
                        dbProcedures.AddInstructorCourseAsync(
                             InstructorId: instructorId,
                             CourseId: instructorId
                            );*/
        }

        public async Task Delete(int courseId, int instructorId)
        {
            await dbProcedures.DeleteInstructorCourseAsync(instructorId, courseId);

        }

        public async Task<List<Course>> ListCource()
        {
            return await db.Courses.ToListAsync();

        }
    }
}
