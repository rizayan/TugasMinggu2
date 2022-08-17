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
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseDAL;
        private readonly IMapper _mapper;

        public CourseController(ICourse courseDAL, IMapper mapper)
        {
            _courseDAL = courseDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {
            var results = await _courseDAL.GetAll();
            var courseDTOs = _mapper.Map<IEnumerable<CourseDTO>>(results);
            return courseDTOs;
        }

       [HttpPost]
        public async Task<ActionResult> Post(CourseCreateDTO CreateDto)
        {
            try
            {
                var newStudent = _mapper.Map<Course>(CreateDto);
                var result = await _courseDAL.Insert(newStudent);
                var studentReadDto = _mapper.Map<CourseDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, studentReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(CourseDTO swordDto)
        {
            try
            {
                var updateSword = new Course
                {
                    Id = swordDto.Id,
                    Title = swordDto.Title,
                    Credits = swordDto.Credits

                };
                var result = await _courseDAL.Update(updateSword);
                return Ok(result); //swordDto
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
                await _courseDAL.Delete(id);
                return Ok($"Data course dengan id {id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IEnumerable<CourseDTO>> Hello(string name)
        {
            List<CourseDTO> swordDtos = new List<CourseDTO>();
            var results = await _courseDAL.GetByName(name);
            foreach (var result in results)
            {
                swordDtos.Add(new CourseDTO
                {
                    Id = result.Id,
                    Title = result.Title,
                    Credits = result.Credits,
                    

                });
            }
            return swordDtos;
        }

        [HttpGet("WithPages")]
        public async Task<IEnumerable<CourseDTO>> GetAllCoursePaging(int page)
        {
            var results = await _courseDAL.GetAllCoursePaging(page);
            var samuraiWithQuoteDtos = _mapper.Map<IEnumerable<CourseDTO>>(results);
            return samuraiWithQuoteDtos;
        }

        [HttpGet("{id}")]
        public async Task<CourseDTO> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _courseDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<CourseDTO>(result);

            return samuraiDTO;
        }

        [HttpGet("WithStudent")]
        public async Task<IEnumerable<CourseWithStudentDTO>> CourseStudent()
        {
            var results = await _courseDAL.CourseByStudent();
            List<CourseWithStudentDTO> readData = new List<CourseWithStudentDTO>();
            foreach (var result in results)
            {
                List<StudentDTO> studentDTOs = new List<StudentDTO>();
                foreach (var student in result.Enrollments)
                {
                    studentDTOs.Add(new StudentDTO
                    {
                        FirstMidName = student.Student.FirstMidName,
                        LastName = student.Student.LastName,
                    });
                }
                readData.Add(new CourseWithStudentDTO
                {
                    Id = result.Id,
                    Title = result.Title,
                    Credits = result.Credits,
                    Students = studentDTOs
                });
            }
            return readData;
        }

    }
}
