using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class InstructorRepo : IInstructorRepo
    {
        ExaminationContext db;
        IExaminationContextProcedures dbProcedures;

        public InstructorRepo(ExaminationContext _db, IExaminationContextProcedures _dbProcedures)
        {
            db = _db;
            dbProcedures = _dbProcedures;
        }

        public async Task<List<GetInstructorDataResult>> GetAll()
        {
            return await dbProcedures.GetInstructorDataAsync();
        }

        public async Task Add(Instructor instructor)
        {
            await dbProcedures.AddInstructorAsync(
                 insFName: instructor.InstructorFname,
                 insLName: instructor.InstructorLname,
                 insSalary: instructor.InstructorSalary,
                 insEmail: instructor.InstructorEmail,
                 insPassword: instructor.InstructorPassword,
                 insGender: instructor.InstructorGender
                 );

        }


        public async Task Edit(int id, Instructor instructor)
        {
            await dbProcedures.UpdateInstructorAsync(
             instructorId: id,
             insFName: instructor.InstructorFname,
             insLName: instructor.InstructorLname,
             insSalary: instructor.InstructorSalary,
             insEmail: instructor.InstructorEmail,
             insPassword: instructor.InstructorPassword
             );
        }
        public void Delete(int id)
        {
            //var InstructorCourses = db.InstructorCourses.Where(sc => sc.InstructorId == id);
            //db.InstructorCourses.RemoveRange(InstructorCourses);
            //db.SaveChanges();

            //db.Database.ExecuteSqlRaw("EXECUTE dbo.DeleteStudent {0}", id);
            //db.SaveChanges();
        }
        public bool IsEmailExist(string email)
        {
            return db.Instructors.Any(a => a.InstructorEmail == email);
        }




    }
}
