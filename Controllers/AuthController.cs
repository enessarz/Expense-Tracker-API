using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> RegisterUser([FromBody] UserRegisterDTO newUser)
        {
            var userExists = await _context.User.AnyAsync(u => u.Id == newUser.Id);
            if (userExists)
            {
                return BadRequest("User already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            var registeredUser = new User
            {
                UserName = newUser.UserName,
                PasswordHash = hashedPassword
            };

            _context.Add(registeredUser);
            await _context.SaveChangesAsync();

            var safeResponse = new UserResponseDTO
            {
                Id = newUser.Id,
                UserName = newUser.UserName
            };

            return Ok(safeResponse);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(UserLoginDTO request)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null)
            {
                return BadRequest("User does not exist.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Secretkey").Value!
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
                audience: _configuration.GetSection("JwtSettings:Audience").Value,
                signingCredentials: creds,
                claims: claims,
                expires: DateTime.Now.AddDays(1)
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}