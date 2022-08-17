using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetByName(string name);
        Task<IEnumerable<Student>> GetByLastName(string name);
        Task<IEnumerable<Student>> GetAllStudentPaging(int page);
        Task<IEnumerable<Student>> StudentWithCourse(int page);
        Task<IEnumerable<Student>> StudentWithCourseWP();

    }
}
