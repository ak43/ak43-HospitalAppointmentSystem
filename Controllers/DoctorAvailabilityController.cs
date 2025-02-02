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
    public class DoctorAvailabilityController : Controller
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly IMapper _mapper;

        public DoctorAvailabilityController(IDoctorAvailabilityRepository doctorAvailabilityRepository,
            IMapper mapper)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<DoctorAvailability>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllAvailabilities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var availabilities = _mapper.Map<List<DoctorAvailabilityDto>>(_doctorAvailabilityRepository.GetAvailabilities());
            if (!availabilities.Any())
                return NotFound();
            return Ok(availabilities);
        }
        [HttpGet("{availabilityId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<DoctorAvailability>))]
        [ProducesResponseType(400)]
        public IActionResult GetAvailability(int availabilityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var availabilities = _mapper.Map<DoctorAvailabilityDto>(_doctorAvailabilityRepository.GetAvailability(availabilityId));
            if (availabilities == null)
                return NotFound();
            return Ok(availabilities);
        }

        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<DoctorAvailability>))]
        [ProducesResponseType(400)]
        public IActionResult GetAvailabilityByDoctor(int doctorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var availabilities = _mapper.Map<List<DoctorAvailabilityDto>>(_doctorAvailabilityRepository.GetAvailabilityByDoctor(doctorId));
            if (availabilities == null)
                return NotFound();
            return Ok(availabilities);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SaveAvailability([FromBody] DoctorAvailability availabilityToSave)
        {
            if (availabilityToSave == null)
                return BadRequest(ModelState);

            var patient = _doctorAvailabilityRepository.GetAvailabilities()
                .FirstOrDefault(p => p.DoctorId == availabilityToSave.DoctorId &&
            p.DayOfWeek == availabilityToSave.DayOfWeek);

            // List.Any() is used to check if IEnumerable is Empty or Not
            //if (patients.Any())
            if (patient != null)
            {
                ModelState.AddModelError("", "Doctor Availability data for this day already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var availabilityMap = _mapper.Map<DoctorAvailability>(availabilityToSave);
            //ownerMap.Country = _countryRepository.GetCountry(countryId);
            if (!_doctorAvailabilityRepository.SaveAvailability(availabilityMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        [HttpPut("{availabilityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoctor(int availabilityId, [FromBody] DoctorAvailabilityDto availabilityUpdated)
        {
            if (availabilityUpdated == null)
                return BadRequest(ModelState);

            if (availabilityId != availabilityUpdated.Id)
                return BadRequest(ModelState);

            if (!_doctorAvailabilityRepository.DoctorAvailabilityExists(availabilityId))
                return NotFound();

            var availabilityMap = _mapper.Map<DoctorAvailability>(availabilityUpdated);
            if (!_doctorAvailabilityRepository.UpdateAvailability(availabilityMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{availabilityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAvailability(int availabilityId)
        {
            if (!_doctorAvailabilityRepository.DoctorAvailabilityExists(availabilityId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var availabilityToDelete = _doctorAvailabilityRepository.GetAvailability(availabilityId);
            //var categoryMap = _mapper.Map
            if (!_doctorAvailabilityRepository.DeleteAvailability(availabilityToDelete))
            {
                ModelState.AddModelError("", "Something wrong while deleting owner.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }
    }
}
