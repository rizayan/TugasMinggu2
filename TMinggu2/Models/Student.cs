using System.ComponentModel.DataAnnotations;

namespace TMinggu2.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstMidName { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
