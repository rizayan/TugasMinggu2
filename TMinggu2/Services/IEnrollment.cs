using TMinggu2.Models;

namespace TMinggu2.Services
{
    public interface IEnrollment
    {
        Task<IEnumerable<Enrollment>> GetAll();
        Task<Enrollment> Insert(Enrollment obj);
    }
}
