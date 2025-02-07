using AutoMapper;
using HospitalAppointmentSystem.Dto;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    //[EnableCors("ApptCorsPolicy")]
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IMapper _mapper;

        public AvailabilityController(IAvailabilityRepository availabilityRepository,
            IMapper mapper)
        {
            _availabilityRepository = availabilityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(ICollection<Availability>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllAvailabilities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var availabilities = _mapper.Map<List<AvailabilityDto>>(_availabilityRepository.GetAvailabilities());
            if (!availabilities.Any())
                return NotFound();
            return Ok(availabilities);
        }
        [HttpGet("{availabilityId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Availability>))]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAvailability(int availabilityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var availabilities = _mapper.Map<AvailabilityDto>(_availabilityRepository.GetAvailability(availabilityId));
            if (availabilities == null)
                return NotFound();
            return Ok(availabilities);
        }

        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Availability>))]
        [ProducesResponseType(400)]
        public IActionResult GetAvailabilityByDoctor(int doctorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var availabilities = _mapper.Map<List<AvailabilityDto>>(_availabilityRepository.GetAvailabilityByDoctor(doctorId));
            if (availabilities == null)
                return NotFound();
            return Ok(availabilities);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SaveAvailability([FromBody] AvailabilityDto availabilityToSave)
        {
            if (availabilityToSave == null)
                return BadRequest(ModelState);

            var patient = _availabilityRepository.GetAvailabilities()
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

            var availabilityMap = _mapper.Map<Availability>(availabilityToSave);
            //ownerMap.Country = _countryRepository.GetCountry(countryId);
            if (!_availabilityRepository.SaveAvailability(availabilityMap))
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
        public IActionResult UpdateDoctor(int availabilityId, [FromBody] AvailabilityDto availabilityUpdated)
        {
            if (availabilityUpdated == null)
                return BadRequest(ModelState);

            if (availabilityId != availabilityUpdated.Id)
                return BadRequest(ModelState);

            if (!_availabilityRepository.DoctorAvailabilityExists(availabilityId))
                return NotFound();

            var availabilityMap = _mapper.Map<Availability>(availabilityUpdated);
            if (!_availabilityRepository.UpdateAvailability(availabilityMap))
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
            if (!_availabilityRepository.DoctorAvailabilityExists(availabilityId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var availabilityToDelete = _availabilityRepository.GetAvailability(availabilityId);
            //var categoryMap = _mapper.Map
            if (!_availabilityRepository.DeleteAvailability(availabilityToDelete))
            {
                ModelState.AddModelError("", "Something wrong while deleting availability.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }
    }
}
