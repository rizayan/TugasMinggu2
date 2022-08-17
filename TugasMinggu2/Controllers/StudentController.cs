using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TugasMinggu2.DAL;
using TugasMinggu2.DTO;
using TugasMinggu2.Models;

namespace TugasMinggu2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentDAL;
        private readonly IMapper _mapper;

        public StudentController(IStudent studentDAL, IMapper mapper)
        {
            _studentDAL = studentDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            var results = await _studentDAL.GetAll();
            var studentDTOs = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return studentDTOs;
        }

        [HttpPost]
        public async Task<ActionResult> Post(StudentCreateDTO CreateDto)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(CreateDto);
                var result = await _studentDAL.Insert(newStudent);
                var studentReadDto = _mapper.Map<StudentDTO>(result);

                return CreatedAtAction("Get", new { id = result.ID }, studentReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _studentDAL.Delete(id);
                return Ok($"Data student dengan id {id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(StudentDTO swordDto)
        {
            try
            {
                var updateSword = new Student
                {
                    ID = swordDto.ID,
                    FirstMidName = swordDto.FirstMidName,
                    LastName = swordDto.LastName,
                   
                };
                var result = await _studentDAL.Update(updateSword);
                return Ok(result); //swordDto
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IEnumerable<StudentDTO>> Hello(string name)
        {
            List<StudentDTO> swordDtos = new List<StudentDTO>();
            var results = await _studentDAL.GetByName(name);
            foreach (var result in results)
            {
                swordDtos.Add(new StudentDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName,
                    EnrollmentDate = result.EnrollmentDate

                });
            }
            return swordDtos;
        }

        [HttpGet("LastName/{Lname}")]
        public async Task<IEnumerable<StudentDTO>> Hello1(string Lname)
        {
            List<StudentDTO> swordDtos = new List<StudentDTO>();
            var results = await _studentDAL.GetByLastName(Lname);
            foreach (var result in results)
            {
                swordDtos.Add(new StudentDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName,
                    EnrollmentDate = result.EnrollmentDate

                });
            }
            return swordDtos;
        }
        [HttpGet("WithPages")]
        public async Task<IEnumerable<StudentDTO>> GetAllStudentPaging(int page)
        {
            var results = await _studentDAL.GetAllStudentPaging(page);
            var samuraiWithQuoteDtos = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return samuraiWithQuoteDtos;
        }

        [HttpGet("WithCourse")]
        public async Task<IEnumerable<StudentWithCourseDTO>> StudentCourse(int page)
        {
            var results = await _studentDAL.StudentWithCourse(page);
            //var readData = _mapper.Map<IEnumerable<StudentWithCourseDTO>>(results);
            // tanpa mapper
            List<StudentWithCourseDTO> readData = new List<StudentWithCourseDTO>();
            foreach (var result in results)
            {
                List<CourseDTO> courseDTOs = new List<CourseDTO>();
                foreach (var enrolment in result.Enrollments)
                {
                    courseDTOs.Add(new CourseDTO
                    {
                        Id = enrolment.CourseID,
                        Title = enrolment.Course.Title,
                        Credits = enrolment.Course.Credits
                    });
                }
                readData.Add(new StudentWithCourseDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName,
                    Enrollments = courseDTOs

                });

            }
            return readData;
        }

        [HttpGet("WithCourseWP")]
        public async Task<IEnumerable<StudentWithCourseDTO>> CourseStudent()
        {
            var results = await _studentDAL.StudentWithCourseWP();
            List<StudentWithCourseDTO> readData = new List<StudentWithCourseDTO>();
            foreach (var result in results)
            {
                List<CourseDTO> courseDTOs = new List<CourseDTO>();
                foreach (var enrolment in result.Enrollments)
                {
                    courseDTOs.Add(new CourseDTO
                    {
                        Id = enrolment.CourseID,
                        Title = enrolment.Course.Title,
                        Credits = enrolment.Course.Credits
                    });
                }
                readData.Add(new StudentWithCourseDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName,
                    Enrollments = courseDTOs

                });

            }
            return readData;
        }

        [HttpPost("AddStudentWithCourse")]
        public async Task<ActionResult> Post(AddStudentWithCourse addSamuraiWithSwordCreateDTO)
        {
            try
            {
                var newSamurai = _mapper.Map<Student>(addSamuraiWithSwordCreateDTO);
                var result = await _studentDAL.Insert(newSamurai);
                var addsamuraiwithswordDto = _mapper.Map<AddStudentWithCourse>(result);

                return Ok("Data Sword dan Samurai berhasil ditambah");
                //CreatedAtAction("Get", new { id = result.Id }, addsamuraiwithswordDto); 

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<StudentDTO> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _studentDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<StudentDTO>(result);

            return samuraiDTO;
        }
    }

}
