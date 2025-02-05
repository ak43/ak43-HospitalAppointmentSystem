using AutoMapper;
using HospitalAppointmentSystem.Dto;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
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
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _mapper.Map<List<User>>(_userRepository.GetUsers());
            if (!users.Any())
                return NotFound();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(_userRepository.GetUser(userId));
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpGet("username/{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(_userRepository.GetUser(username));
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // Get User detail by user's first name & last name
        [HttpGet("name/{firstName}&{lastName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserUsername(string firstName, string lastName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(_userRepository.GetUserByName(firstName, lastName));
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult SaveUser([FromBody] UserDto userToSave )
        {
            if (userToSave == null)
                return BadRequest(ModelState);

            // User account is created for existing person - registered person
            //var person = _userRepository.GetUser
            var user = _userRepository.GetUser(userToSave.Username);
            if(user != null)
            {
                ModelState.AddModelError("", "Username already exisits");
                return StatusCode(422, ModelState);
            }
            var userMap = _mapper.Map<User>(userToSave);
            if (user != null)
            {
                var user2 = _userRepository.GetUserByName(user.Person.FirstName, user.Person.LastName);
                if (user2 != null)
                {
                    ModelState.AddModelError("", "The person has existing user account.");
                    return StatusCode(422, ModelState);
                }
            }
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_userRepository.SaveUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("User account created.");
        }
    }
}
