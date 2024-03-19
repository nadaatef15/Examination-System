using Exam_System.IRepository;
using Exam_System.Models;

namespace Exam_System.Repository
{
    public class InstructorRepo : IInstructorRepo
    {
        readonly ExaminationContext db;
        readonly IExaminationContextProcedures dbProcedures;

        public InstructorRepo(ExaminationContext _db, IExaminationContextProcedures _dbProcedures)
        {
            db = _db;
            dbProcedures = _dbProcedures;
        }

        public async Task<List<GetInstructorDataResult>> GetAll() =>
             await dbProcedures.GetInstructorDataAsync();

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

        public async Task Delete(int id) =>
            await dbProcedures.DeleteInstructorAsync(id);

        public bool IsEmailExist(string email) =>
            db.Instructors.Any(a => a.InstructorEmail == email);
    }
}
