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
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollment _enrollmentDAL;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollment enrollmentDAL, IMapper mapper)
        {
            _enrollmentDAL = enrollmentDAL;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<EnrollmentDTO>> Get()
        {
            var results = await _enrollmentDAL.GetAll();
            var enrollmentDTOs = _mapper.Map<IEnumerable<EnrollmentDTO>>(results);
            return enrollmentDTOs;
        }

        [HttpGet("{id}")]
        public async Task<EnrollmentDTO> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _enrollmentDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<EnrollmentDTO>(result);

            return samuraiDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EnrollmentCreateDTO enrollmentCreateDTO)
        {
            try
            {
                var newEnrollment = _mapper.Map<Enrollment>(enrollmentCreateDTO);
                var result = await _enrollmentDAL.Insert(newEnrollment);
                var readData = _mapper.Map<EnrollmentDTO>(result);
                return CreatedAtAction("Get", new { Id = result.EnrollmentID }, readData);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _enrollmentDAL.Delete(id);
                return Ok("Data Berhasil di Hapus");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(EnrollmentDTO courseDTO)
        {
            try
            {
                var updateUser = _mapper.Map<Enrollment>(courseDTO);
                var result = await _enrollmentDAL.Update(updateUser);
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
