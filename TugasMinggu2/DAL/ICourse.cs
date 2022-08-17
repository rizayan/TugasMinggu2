using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByName(string name);
        Task<IEnumerable<Course>> GetAllCoursePaging(int page);
        Task<IEnumerable<Course>> CourseByStudent();
    }
}
