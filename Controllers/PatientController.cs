using AutoMapper;
using HospitalAppointmentSystem.Dto;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<PatientDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatients() {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var patients = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatients());
            if (patients.Count == 0)
                return NotFound();
            return Ok(patients);
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult GetPatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var patient = _mapper.Map<PatientDto>(_patientRepository.GetPatient(patientId));
            return Ok(patient);
        }

        [HttpGet("name/{firstName}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Doctor>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientByName(string firstName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var patients = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatients(firstName));
            if (patients.Count == 0)
                return NotFound();
            return Ok(patients);
        }

        [HttpGet("dept/{deptId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Patient>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatients(int deptId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var patients = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatientByDepartment(deptId));
            if (patients.Count == 0)
                return NotFound();
            return Ok(patients);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SavePatient(int doctorId, [FromBody] PatientDto patientToSave) 
        {
            if (patientToSave == null)
                return BadRequest(ModelState);

            var patient = _patientRepository.GetPatients()
                .FirstOrDefault(p => p.FirstName.Trim().ToUpper() == patientToSave.FirstName.Trim().ToUpper() &&
            p.LastName.Trim().ToUpper() == patientToSave.LastName.Trim().ToUpper()); 
            
            // List.Any() is used to check if IEnumerable is Empty or Not
            //if (patients.Any())
                if (patient != null)
                {
                ModelState.AddModelError("", "Patient with the same name already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientMap = _mapper.Map<Patient>(patientToSave);
            //ownerMap.Country = _countryRepository.GetCountry(countryId);
            if (!_patientRepository.SavePatient(doctorId, patientMap)) 
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        [HttpPut("{patientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoctor(int patientId, [FromBody] PatientDto patientUpdated)
        {
            if (patientUpdated == null)
                return BadRequest(ModelState);

            if (patientId != patientUpdated.Id)
                return BadRequest(ModelState);

            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var patientMap = _mapper.Map<Patient>(patientUpdated); 
            if (!_patientRepository.UpdatePatient(patientMap)) 
            { 
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{patientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patienToDelete = _patientRepository.GetPatient(patientId);
            //var categoryMap = _mapper.Map
            if (!_patientRepository.DeletePatient(patienToDelete))
            {
                ModelState.AddModelError("", "Something wrong while deleting owner.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }
    }
}
