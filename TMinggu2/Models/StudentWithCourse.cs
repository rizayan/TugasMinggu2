namespace TMinggu2.Models
{
    public class StudentWithCourse
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Course> Enrollments { get; set; }
    }
}
