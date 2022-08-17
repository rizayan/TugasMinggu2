using AutoMapper;
using TugasMinggu2.DTO;
using TugasMinggu2.Models;

namespace TugasMinggu2.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<StudentCreateDTO, Student>();
            CreateMap<Student, StudentWithCourseDTO>();
            CreateMap<Enrollment, CourseDTO>();

            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<CourseCreateDTO, Course>();

            CreateMap<Enrollment, EnrollmentDTO>();
            CreateMap<EnrollmentDTO, Enrollment>();
            CreateMap<EnrollmentCreateDTO, Enrollment>();
        }
    }
}
