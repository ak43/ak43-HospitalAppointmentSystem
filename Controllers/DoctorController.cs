using AutoMapper;
using HospitalAppointmentSystem.Dto;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HospitalAppointmentSystem.Controllers
{ 
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : Controller 
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<DoctorDto>))]
        [ProducesResponseType(400)]   
        public async Task<IActionResult> GetDoctors()
            {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var doctors = _mapper.Map<List<DoctorDto>>(await _doctorRepository.GetDoctors());
            if (doctors.Count == 0)
                return NotFound();
            return Ok(doctors);
            }

        [HttpGet("{doctorId}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctor (int doctorId)
        {
            if (!await _doctorRepository.DoctorExists(doctorId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var doctor = _mapper.Map<DoctorDto>(await _doctorRepository.GetDoctor(doctorId));
            return Ok(doctor);
        }

        [HttpGet("name/{firstName}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctorByName(string firstName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var doctors = _mapper.Map<List<DoctorDto>>(await _doctorRepository.GetDoctors(firstName));
            if (doctors.Count == 0)
                return NotFound();
            return Ok(doctors);
        }

        [HttpGet("dept/{deptId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Doctor>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctors(int deptId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var doctors = _mapper.Map<List<DoctorDto>>(await _doctorRepository.GetDoctorByDepartment(deptId));
            if (doctors.Count == 0)
                return NotFound();
            return Ok(doctors);
        }

        [HttpGet("specialty/{specialty}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctors(string specialty)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var doctors = _mapper.Map<List<DoctorDto>>(await _doctorRepository.GetDoctorBySpeciality(specialty));
            if (doctors.Count == 0)
                return NotFound();
            return Ok(doctors);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveDoctor([FromBody] DoctorDto doctorToSave)
        {
            if (doctorToSave == null)
                return BadRequest(ModelState);
            //var doctor = await _doctorRepository.GetDoctors().Where(d => d.FirstName.Trim().ToUpper() == doctorToSave.FirstName.Trim().ToUpper() &&
            //d.LastName.Trim().ToUpper() == doctorToSave.LastName.Trim().ToUpper() &&
            //d.Specialization == doctorToSave.Specialization.Trim().ToUpper()).ToList();

            var doctors = await _doctorRepository.GetDoctors();

            var doctor = doctors.Where(d => d.FirstName.Trim().ToUpper() == doctorToSave.FirstName.Trim().ToUpper() &&
            d.LastName.Trim().ToUpper() == doctorToSave.LastName.Trim().ToUpper() &&
            d.Specialization == doctorToSave.Specialization.Trim().ToUpper()).ToList();

            if (doctor.Any())
            {
                ModelState.AddModelError("", "Doctor already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doctorMap = _mapper.Map<Doctor>(doctorToSave);
            //ownerMap.Country = _countryRepository.GetCountry(countryId);
            if (!await _doctorRepository.CreateDoctor(doctorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        [HttpPut("{doctorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> UpdateDoctor(int doctorId, [FromBody] DoctorDto doctorUpdated)
        {
            if (doctorUpdated == null)
                return BadRequest(ModelState);

            if (doctorId != doctorUpdated.Id)
                return BadRequest(ModelState);

            if (!await _doctorRepository.DoctorExists(doctorId))
                return NotFound();

            var doctorMap = _mapper.Map<Doctor>(doctorUpdated);
            if (!await _doctorRepository.UpdateDoctor(doctorMap))
            {
                ModelState.AddModelError("", "Something wen wrong while updating.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{doctorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            if (!await _doctorRepository.DoctorExists(doctorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doctorToDelete = await _doctorRepository.GetDoctor(doctorId);
            //var categoryMap = _mapper.Map
            if (!await _doctorRepository.DeleteDoctor(doctorToDelete))
            {
                ModelState.AddModelError("", "Something wrong while deleting owner.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }
    }
}
