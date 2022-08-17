namespace TMinggu2.Models
{
    public class CourseWithStudent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
