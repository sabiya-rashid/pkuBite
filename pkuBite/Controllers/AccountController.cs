using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pkuBite.Data;
using pkuBite.Dto;
using pkuBite.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pkuBite.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AccountController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            string passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            //if(request.Username != null)
            //{
            //    return BadRequest(ModelState);
            //}
            var user = new User();
            user.Username = request.Username;
            user.PasswordHash = passwordhash;
            _context.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            var user = _context.Users.FirstOrDefault(c => c.Username == request.Username);
            if (user == null)
            {
                return BadRequest("No user found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest("Wrong Password");
            string token = CreateToken(user);
            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
