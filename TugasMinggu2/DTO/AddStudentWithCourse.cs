namespace TugasMinggu2.DTO
{
    public class AddStudentWithCourse
    {
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<CourseDTO> Enrollments { get; set; }

    }
}
