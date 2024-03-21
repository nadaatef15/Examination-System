using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<InstructorCourse> getByCourseId(int courseId)
        {
            return db.InstructorCourses.Where(a => a.CourseId == courseId).ToList();
        }
        public List<InstructorCourse>getByinstructotId(int instructorId)
        {
            return db.InstructorCourses.Where(a => a.InstructorId == instructorId).ToList();

        }
        public async Task getByInstrucrorId(int instructorId)
        {
            await dbProcedures.GetInstrustureByIdAsync(instructorId);
        }

        public async Task<List<GetAllInstructorsCoursesResult>> getAll() => 
               await dbProcedures.GetAllInstructorsCoursesAsync();


        public List<InstructorCourse> getByCourseByInstructor(int instructorid)
        {
            return db.InstructorCourses.FromSqlRaw("EXECUTE dbo.GetAllInstructorCourses {0}", instructorid).ToList();
        }

        public void Add(InstructorCourse instructorCourse)
        {

            dbProcedures.AddInstructorCourseAsync(
                 InstructorId: instructorCourse.InstructorId,
                 CourseId: instructorCourse.CourseId
                );

        }
        public void Delete(int courseId, int instructorId)
        {
            dbProcedures.DeleteInstructorCourseAsync(instructorId, courseId);

        }
    }
}
