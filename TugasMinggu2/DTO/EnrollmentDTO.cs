using TugasMinggu2.DAL;
using TugasMinggu2.Models;

namespace TugasMinggu2.DTO
{
    public class EnrollmentDTO
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
    }
}
