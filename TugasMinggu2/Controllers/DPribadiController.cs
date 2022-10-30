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
    public class DPribadiController : ControllerBase
    {
        private readonly IDPribadi _dpribadi;
        private readonly IMapper _mapper;

        public DPribadiController(IDPribadi dpribadi, IMapper mapper)
        {
            _dpribadi = dpribadi;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<DPribadi>> Get()
        {
            var results = await _dpribadi.GetAll();
            var studentDTOs = _mapper.Map<IEnumerable<DPribadi>>(results);
            return studentDTOs;
        }

        [HttpGet("ByName/{name}")]
        public async Task<IEnumerable<DPribadi>> Hello(string name)
        {
            List<DPribadi> swordDtos = new List<DPribadi>();
            var results = await _dpribadi.GetByName(name);
            foreach (var result in results)
            {
                swordDtos.Add(new DPribadi
                {
                    Id = result.Id,
                    NamaLengkap = result.NamaLengkap,
                    JenisKelamin = result.JenisKelamin,
                    TanggalLahir = result.TanggalLahir,
                    Alamat = result.Alamat,
                    Negara = result.Negara

                });
            }
            return swordDtos;
        }

        [HttpGet("{id}")]
        public async Task<DPribadi> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _dpribadi.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<DPribadi>(result);

            return samuraiDTO;
        }

        [HttpGet("n/{nik}")]
        public async Task<DPribadi> GetbyNIK(int nik)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _dpribadi.GetByNIK(nik);
            if (result == null) throw new Exception($"data {nik} tidak ditemukan");
            var samuraiDTO = _mapper.Map<DPribadi>(result);

            return samuraiDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post(DPribadiCreateDTO CreateDto)
        {
            try
            {
                var newStudent = _mapper.Map<DPribadi>(CreateDto);
                var result = await _dpribadi.Insert(newStudent);
                var studentReadDto = _mapper.Map<DPribadiCreateDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, studentReadDto);
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
                await _dpribadi.Delete(id);
                return Ok($"Data student dengan id {id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(DPribadi swordDto)
        {
            try
            {
                var updateSword = new DPribadi
                {
                    Id = swordDto.Id,
                    NIK = swordDto.NIK,
                    NamaLengkap = swordDto.NamaLengkap,
                    JenisKelamin = swordDto.JenisKelamin,
                    TanggalLahir = swordDto.TanggalLahir,
                    Alamat = swordDto.Alamat,
                    Negara = swordDto.Negara

                };
                var result = await _dpribadi.Update(updateSword);
                return Ok(result); //swordDto
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
