using Microsoft.EntityFrameworkCore;
using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public class DPribadiDAL : IDPribadi
    {
        private readonly AppDbContext _context;

        public DPribadiDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.DPribadis.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                    throw new Exception($"Data student dengan id {id} tidak ditemukan");
                _context.DPribadis.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<DPribadi>> GetAll()
        {
            var results = await _context.DPribadis.OrderBy(s => s.NamaLengkap).ToListAsync();
            return results;
        }

        public async Task<DPribadi> GetById(int id)
        {
            var result = await _context.DPribadis.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<DPribadi>> GetByName(string name)
        {
            var swords = await _context.DPribadis.Where(s => s.NamaLengkap.Contains(name))
               .OrderBy(s => s.NamaLengkap).ToListAsync();
            return swords;
        }


        public async Task<DPribadi> GetByNIK(int nik)
        {
            var result = await _context.DPribadis.FirstOrDefaultAsync(s => s.NIK == nik);
            if (result == null) throw new Exception($"Data pribadi dengan id {nik} tidak ditemukan");
            return result;
        }

        public async Task<DPribadi> Insert(DPribadi obj)
        {
            try
            {
                _context.DPribadis.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<DPribadi> Update(DPribadi obj)
        {
            try
            {
                var updateSword = await _context.DPribadis.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data Course dengan id {obj.Id} tidak ditemukan");

                updateSword.NamaLengkap = obj.NamaLengkap;
                updateSword.JenisKelamin = obj.JenisKelamin;
                updateSword.TanggalLahir = obj.TanggalLahir;
                updateSword.Alamat = obj.Alamat;
                updateSword.Negara = obj.Negara;

                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }

       
        }
 
