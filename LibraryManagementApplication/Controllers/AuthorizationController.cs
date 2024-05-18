using LibraryManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementApplication.Data;
using LibraryManagementApplication.Models;

namespace LibraryManagementApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly LibraryManagementDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly string _adminKey;

        /// <summary>
        /// Constructor to initialize the AuthorizationController with necessary dependencies.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtTokenGenerator"></param>
        /// <param name="configuration"></param>
        
        public AuthorizationController(LibraryManagementDbContext context, IJwtTokenGenerator jwtTokenGenerator, IConfiguration configuration)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _adminKey = configuration["AdminKey"];
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The result of the registration process.</returns>
       
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDetails request)
        {
            // Check if the role is Admin and the AdminKey is provided
            if (request.Role == "Admin" && request.AdminKey != _adminKey)
            {
                return Unauthorized("Invalid admin key");
            }

            var existingUser = _context.Users.SingleOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var user = new UserInfo
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The result of the login process with a JWT token if successful.</returns>
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDetails request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized();
            }

            var token = _jwtTokenGenerator.GenerateToken(user.Username, user.Role);
            return Ok(new { Token = token });
        }
    }
}
