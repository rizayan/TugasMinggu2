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
    }
}
