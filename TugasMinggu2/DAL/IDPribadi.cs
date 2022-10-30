using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public interface IDPribadi : ICrud<DPribadi>
    {
        Task<IEnumerable<DPribadi>> GetByName(string name);
        Task<DPribadi> GetByNIK(int nik);
    }
}
