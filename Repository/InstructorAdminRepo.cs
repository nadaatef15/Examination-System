using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class InstructorAdminRepo : IInstructorAdminRepo
    {
        readonly ExaminationContext db;
        readonly IExaminationContextProcedures dbProcedures;

        public InstructorAdminRepo(ExaminationContext _db, IExaminationContextProcedures _dbProcedures)
        {
            db = _db;
            dbProcedures = _dbProcedures;
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

        public async Task Delete(int id) =>
            await dbProcedures.DeleteInstructorAsync(id);

        public bool IsEmailExist(string email) =>
            db.Instructors.Any(a => a.InstructorEmail == email);


        public async Task<List<GetInstructorDataResult>> GetAll() =>
            await dbProcedures.GetInstructorDataAsync();

        public Instructor GetById(int id)
        {

            //dbProcedures.GetInstrustureByIdAsync(id)
            return db.Instructors.FromSqlRaw("EXECUTE dbo.GetInstrustureById {0}", id).AsEnumerable().FirstOrDefault();


        }



        // [GetTrackByInstructorId]
        /*public async Task< List<GetTrackByInstructorIdAsync>> GetTracks(Instructor id)=>
             await dbProcedures.GetTrackByInstructorIdAsync(id);
*/

        public  List <Track> GetTracks(int id)
        {
            return db.Tracks.FromSqlRaw("EXECUTE dbo.GetTrackByInstructorIdAsync {0}" , SupervisorId).AsEnumerable().FirstOrDefault();
        }





    }
}
