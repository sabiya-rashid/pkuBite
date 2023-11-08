using AutoWrapper.Wrappers;
using Azure.Core;
using Common.DTOs.Account;
using DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pkuBite.Models;
using Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pkuBite.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        [HttpPost("register")]
        public async Task<ApiResponse> Register(RegisterDto userDto)
        {
            return await _accountServices.Register(userDto);
        }
        [HttpPost("login")]
        public async Task<ApiResponse> Login(UserDto userDto)
        {
            return await _accountServices.Login(userDto.Username, userDto.Password);
        }        
    }
}
