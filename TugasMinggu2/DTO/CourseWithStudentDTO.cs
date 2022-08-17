namespace TugasMinggu2.DTO
{
    public class CourseWithStudentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<StudentDTO> Students { get; set; }
    }
}
