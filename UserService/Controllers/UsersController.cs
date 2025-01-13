using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersService.Data;
using UsersService.Models;
using UsersService.Services;
using UsersService.Dtos;
using UsersService.Dtos;
namespace UsersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly int _expirationDays;

        public UsersController(IUserRepository userRepository,
                        ITokenService tokenService,
                       IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _expirationDays = config.GetValue<int>("TokenSettings:ExpirationDays");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Dtos.RegisterRequest request)
        {
            if (await _userRepository.EmailExists(request.Email))
                return BadRequest("Email already exists.");

            var passwordHasher = new PasswordHasher<string>();
            var hashedPassword = passwordHasher.HashPassword(null, request.Password);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Email = request.Email
            };

            await _userRepository.Add(user);
            await _userRepository.Save();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Dtos.LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null) return Unauthorized("Invalid email or password.");

            var passwordHasher = new PasswordHasher<string>();
            var verificationResult = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, request.Password);

            if (verificationResult != PasswordVerificationResult.Success)
                return Unauthorized("Invalid username or password.");

            // Generate JWT token
            var token = _tokenService.CreateToken(user);
            return Ok(new { Token = token, Username = user.Username, ExpireDate = DateTime.Now.AddDays(_expirationDays) });
        }
    }
}
