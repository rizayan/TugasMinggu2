using Microsoft.EntityFrameworkCore;
using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public class StudentDAL : IStudent
    {
        private readonly AppDbContext _context;

        public StudentDAL(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                if (deleteSword == null)
                    throw new Exception($"Data student dengan id {id} tidak ditemukan");
                _context.Students.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _context.Students.OrderBy(s => s.LastName).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Student>> GetAllStudentPaging(int page)
        {
            var pageresult = 2f;
            var pagecount = Math.Ceiling(_context.Students.Count() / pageresult);
            var samurais = await _context.Students.OrderBy(s => s.FirstMidName).
                Skip((page - 1) * (int)pageresult).
                Take((int)pageresult).
                ToListAsync();
            return samurais;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (result == null) throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Student>> GetByName(string name)
        {
            var swords = await _context.Students.Where(s => s.FirstMidName.Contains(name))
               .OrderBy(s => s.FirstMidName).ToListAsync();
            return swords;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _context.Students.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Student> Update(Student obj)
        {
            try
            {
                var updateSword = await _context.Students.FirstOrDefaultAsync(s => s.ID == obj.ID);
                if (updateSword == null)
                    throw new Exception($"Data Student dengan id {obj.ID} tidak ditemukan");

                updateSword.FirstMidName = obj.FirstMidName;
                updateSword.LastName = obj.LastName;
               
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> StudentWithCourse(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Students.Count() / pageResults);

            var results = await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Student>> GetByLastName(string Lname)
        {
            var swords = await _context.Students.Where(s => s.LastName.Contains(Lname))
               .OrderBy(s => s.LastName).ToListAsync();
            return swords;
        }

        public async Task<IEnumerable<Student>> StudentWithCourseWP()
        {
            var results = await _context.Students.Include(c => c.Enrollments).ThenInclude(e => e.Course).ToListAsync();
            return results;
        }
    }
}
