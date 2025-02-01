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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<DepartmentDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartments()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());
            if(departments == null) 
                return NotFound();
            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(DepartmentDto))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int departmentId) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();
            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartment(departmentId));
            //if(department != null)
                return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SaveDepartment([FromBody] DepartmentDto departmentToSave)
        {
            if (departmentToSave == null)
                return BadRequest(ModelState);

            var department = _departmentRepository.GetDepartments().Where(p => p.Name.Trim().ToUpper() == departmentToSave.Name.Trim().ToUpper());

            if (department.Any())
            {
                ModelState.AddModelError("", "Department with the same name already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentToSave);
            if (!_departmentRepository.SaveDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        [HttpPut("{departmentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDto departmentUpdated)
        {
            if (departmentUpdated == null)
                return BadRequest(ModelState);

            if (departmentId != departmentUpdated.Id)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var departmentMap = _mapper.Map<Department>(departmentUpdated);
            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);
            var departmentMap = _mapper.Map<Department>(departmentToDelete);
            if (!_departmentRepository.DeleteDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something wrong while deleting owner.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }

    }
}
