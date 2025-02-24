﻿using AutoMapper;
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
    [Authorize]
    //[EnableCors("ApptCorsPolicy")]
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ILogger<AvailabilityController> _logger;
        private readonly IMapper _mapper;

        public AvailabilityController(IAvailabilityRepository availabilityRepository,
            ILogger<AvailabilityController> logger,
            IMapper mapper)
        {
            _availabilityRepository = availabilityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<AvailabilityDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllAvailabilities()
        {
            _logger.LogInformation("Gettig all availabilities ... ");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Bad State {ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            var avails = await _availabilityRepository.GetAvailabilities();
            var availabilities =_mapper.Map<List<AvailabilityDto>>(avails);
            //var availabilities =  await _mapper.Map<List<AvailabilityDto>>(_availabilityRepository.GetAvailabilities());
            if (!availabilities.Any())
                return NotFound();
            _logger.LogInformation("Availabilities Found .... Returning....");
            return Ok(availabilities);
        }

        [HttpGet("{availabilityId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<AvailabilityDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAvailability(int availabilityId)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Bad State {ModelState} ", ModelState);
                return BadRequest(ModelState);
            }
            //var availabilities = await _mapper.Map<AvailabilityDto>(_availabilityRepository.GetAvailability(availabilityId));
            var avails = await _availabilityRepository.GetAvailability(availabilityId);
            var availabilities = _mapper.Map<AvailabilityDto>(avails);
            if (availabilities == null)
                return NotFound();
            _logger.LogInformation("Availability Found ........");
            return Ok(availabilities);
        }

        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Availability>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAvailabilityByDoctor(int doctorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //var availabilities = _mapper.Map<List<AvailabilityDto>>(_availabilityRepository.GetAvailabilityByDoctor(doctorId));
            var avails = await _availabilityRepository.GetAvailabilityByDoctor(doctorId);
            var availabilities = _mapper.Map<List<AvailabilityDto>>(avails);
            if (availabilities == null)
                return NotFound();
            return Ok(availabilities);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> SaveAvailability([FromBody] AvailabilityDto availabilityToSave)
        {
            if (availabilityToSave == null)
                return BadRequest(ModelState);

    //        var patient = await _availabilityRepository.GetAvailabilities()
    //.FirstOrDefaultAsync(p => p.DoctorId == availabilityToSave.DoctorId &&
    //                          p.DayOfWeek == availabilityToSave.DayOfWeek); ; 

            var avails = await _availabilityRepository.GetAvailabilities();
            var patient = avails.FirstOrDefault(p => p.DoctorId == availabilityToSave.DoctorId &&
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
            if (!await _availabilityRepository.SaveAvailability(availabilityMap))
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
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateAvailability(int availabilityId, [FromBody] AvailabilityDto availabilityUpdated)
        {
            if (availabilityUpdated == null)
                return BadRequest(ModelState);

            if (availabilityId != availabilityUpdated.Id)
                return BadRequest(ModelState);

            if (!await _availabilityRepository.DoctorAvailabilityExists(availabilityId))
                return NotFound();

            var availabilityMap = _mapper.Map<Availability>(availabilityUpdated);
            if (!await _availabilityRepository.UpdateAvailability(availabilityMap))
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAvailability(int availabilityId)
        {
            if (!await _availabilityRepository.DoctorAvailabilityExists(availabilityId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var availabilityToDelete = await _availabilityRepository.GetAvailability(availabilityId);
            //var categoryMap = _mapper.Map
            if (!await _availabilityRepository.DeleteAvailability(availabilityToDelete))
            {
                ModelState.AddModelError("", "Something wrong while deleting availability.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }
    }
}
