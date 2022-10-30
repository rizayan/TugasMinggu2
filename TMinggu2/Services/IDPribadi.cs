using TMinggu2.Models;

namespace TMinggu2.Services
{
    public interface IDPribadi
    {
        Task<IEnumerable<DPribadi>> GetAll();
        Task<DPribadi> GetById(int id);
        Task<DPribadi> GetByNIK(int nik);
        Task<DPribadi> Insert(DPribadi obj);
        Task<DPribadi> Update(DPribadi obj);
        Task Delete(int id);
        Task<IEnumerable<DPribadi>> GetByName(string name);
    }
}
