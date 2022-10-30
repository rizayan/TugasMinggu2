using Microsoft.EntityFrameworkCore;
using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly AppDbContext _context;

        public EnrollmentDAL(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == id);
                if (deleteSword == null)
                    throw new Exception($"Data student dengan id {id} tidak ditemukan");
                _context.Enrollments.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _context.Enrollments.OrderBy(s => s.EnrollmentID).ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var result = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == id);
            if (result == null) throw new Exception($"Data course dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _context.Enrollments.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Enrollment> Update(Enrollment obj)
        {
            try
            {
                var findData = await _context.Enrollments.SingleOrDefaultAsync(s => s.EnrollmentID == obj.EnrollmentID);
                if (findData == null) throw new Exception($"Data dengan ID {obj.EnrollmentID} Tidak Ditemukan");

                findData.CourseID = obj.CourseID;
                findData.StudentID = obj.StudentID;
                findData.Grade = obj.Grade;

                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
