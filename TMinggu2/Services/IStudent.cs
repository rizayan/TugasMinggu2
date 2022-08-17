using TMinggu2.Models;

namespace TMinggu2.Services
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<Student> Insert(Student obj);
        Task<Student> Update(Student obj);
        Task Delete(int id);
        Task<IEnumerable<Student>> GetByName(string name);
        Task<IEnumerable<CourseWithStudent>> GetCourseWithStudent();
        Task<IEnumerable<StudentWithCourse>> GetStudentWithCourse();
        Task AddStudentCourse(string name, string course);
    }
}
