using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;
using HospitalAppointmentSystem.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserRepository _userRepository;

       public AuthController(JwtService jwtService, ILogger<AuthController> logger,  IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation(".... Trying to Login .....");
            // Validate user credetial 
            var user = _userRepository.GetUser(request.Username);
            _logger.LogInformation("... trying to fetch user using username ....");

            //if (request.Username == "admin" && request.Password == "password")
            if (user != null && user.Password == request.Password)
            {
                var token = _jwtService.GenerateToken(request.Username, "Admin");
                _logger.LogInformation("... Login success! ..... Generating Token....");
                return Ok(new { Token = token });
            }

            _logger.LogInformation("ERR: Login not successful ...");
            return Unauthorized();
        }

        //public User CheckUser(LoginRequest request)
        //{
        //    var user = _userRepository.GetUser(request.Username);
        //    if (user.Password == request.Password)
        //        return user;
        //    return null;
        //}

    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
