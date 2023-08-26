
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pkuBite.Data;
using pkuBite.DTO;
using pkuBite.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pkuBite.Controllers
{

    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO.Email == null || loginDTO.Password == null)
            {
                return BadRequest("please enter email and password");
            }

            var user = _db.Users.FirstOrDefault(u=>u.email.ToLower() == loginDTO.Email.ToLower());
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.passwordHash))
            {
                return BadRequest("Email or passwprd is incorrect");
            }

            var token = CreateToken(user);

            var response = new
            {
                StatusCode = 202,
                Message = "User logged in succesfully",
                Data = user,
                Token = token
            };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status202Accepted
            };
        }

        [HttpPost]
        [Route("/signup")]
        public IActionResult Signup([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Incorrect Data passed");
            }

            var user = _db.Users.FirstOrDefault(u => u.email.ToLower() == userDTO.Email.ToLower());

            if (user != null)
            {
                return BadRequest("Email already exists");
            }
             
            var newUser = new User
            {
                email = userDTO.Email,
                passwordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                Role = userDTO.Role ?? "User"
        };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            var token = CreateToken(newUser);

            var response = new
            {
                StatusCode = 201,
                Message = "User created",
                Data = newUser,
                Token = token
            };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _db.Users;
        }


        // -------- Generate the JWT Token ---- //
        private string CreateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Role, user.Role.ToLower()),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }



}

