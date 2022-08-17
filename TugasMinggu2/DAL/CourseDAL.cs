using Microsoft.EntityFrameworkCore;
using TugasMinggu2.Models;

namespace TugasMinggu2.DAL
{
    public class CourseDAL : ICourse
    {
        private readonly AppDbContext _context;

        public CourseDAL(AppDbContext context)
        {
            _context = context;
        }

       

         public async Task Delete(int id)
 {
     try
     {
         var deleteSword = await _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
         if (deleteSword == null)
             throw new Exception($"Data student dengan id {id} tidak ditemukan");
         _context.Courses.Remove(deleteSword);
         await _context.SaveChangesAsync();
     }
     catch (Exception ex)
     {
         throw new Exception($"{ex.Message}");
     }
 }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _context.Courses.OrderBy(s => s.Title).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Course>> GetAllCoursePaging(int page)
        {
            var pageresult = 2f;
            var pagecount = Math.Ceiling(_context.Courses.Count() / pageresult);
            var samurais = await _context.Courses.OrderBy(s => s.Title).
                Skip((page - 1) * (int)pageresult).
                Take((int)pageresult).
                ToListAsync();
            return samurais;
        }

        public async Task<Course> GetById(int id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data course dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Course>> GetByName(string name)
        {
            var swords = await _context.Courses.Where(s => s.Title.Contains(name))
              .OrderBy(s => s.Title).ToListAsync();
            return swords;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _context.Courses.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

       
        public async Task<Course> Update(Course obj)
         {
             try
             {
                 var updateSword = await _context.Courses.FirstOrDefaultAsync(s => s.Id == obj.Id);
                 if (updateSword == null)
                     throw new Exception($"Data Course dengan id {obj.Id} tidak ditemukan");

                 updateSword.Title = obj.Title;
                 updateSword.Credits = obj.Credits;

                 await _context.SaveChangesAsync();
                 return obj;
             }
             catch (Exception ex)
             {
                 throw new Exception($"{ex.Message}");
             }
         }

        public async Task<IEnumerable<Course>> CourseByStudent()
        {
            var results = await _context.Courses.Include(c => c.Enrollments).ThenInclude(e => e.Student).ToListAsync();
            return results;
        }
    }
}
