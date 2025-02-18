using AutoMapper;
using HospitalAppointmentSystem.Dto;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<UserDto>))]
        [ProducesResponseType(400)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsers()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
            if (!users.Any())
                return NotFound();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetUser(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(userId));
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpGet("username/{username}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetUser(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(username));
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // Get User detail by user's first name & last name
        [HttpGet("name/{firstName}&{lastName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetUsername(string firstName, string lastName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<UserDto>(await _userRepository.GetUserByName(firstName, lastName));
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveUser([FromBody] UserDto userToSave )
        {
            if (userToSave == null)
                return BadRequest(ModelState);

            // User account is created for existing person - registered person
            //var person = _userRepository.GetUser
             var user = await _userRepository.GetUser(userToSave.Username);
            if(user != null)
            {
                ModelState.AddModelError("", "Username already exisits");
                return StatusCode(422, ModelState);
            }
            // CHECK if the person has already got a username
            var user1 = await _userRepository.GetUserByPerson(userToSave.PersonId);
            if(user1 != null)
            {
                ModelState.AddModelError("", "The person has existing user acount.");
                return StatusCode(422, ModelState);
            }
            var userMap = _mapper.Map<User>(userToSave);
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _userRepository.SaveUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("User account created.");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userUpdated)
        {
            if (userUpdated == null)
                return BadRequest(ModelState);

            if (userId != userUpdated.Id)
                return BadRequest(ModelState);

            if (!await _userRepository.UserExists(userId))
                return NotFound();

            var userMap = _mapper.Map<User>(userUpdated);
            if (!await _userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!await _userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToDelete = await _userRepository.GetUser(userId);
            //var categoryMap = _mapper.Map
            if (!await _userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something wrong while deleting user.");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Succcess.");
        }
    }
}
