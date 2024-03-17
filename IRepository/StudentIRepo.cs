using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface StudentIRepo
    {
        public List<Student> getAll();
        public Student getById (int id);
        public void Add(Student student);
        public void Edit(int id,Student student);
        public void Delete(int id);
        public bool EmailIsExist(string email);

    }
}
