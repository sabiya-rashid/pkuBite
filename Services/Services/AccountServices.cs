using AutoWrapper.Wrappers;
using Azure.Core;
using BCrypt.Net;
using Common.DTOs.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pkuBite.Models;
using Repository.Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IRepository<User> _repository;
        private readonly IConfiguration _configuration;
        public AccountServices(IRepository<User>  repository, IConfiguration configuration)
        {
            _repository = repository;  
            _configuration = configuration;
        }
        public Task<ApiResponse> Login(string username, string password)
        {
            var user = _repository.GetUser(username);
            if (user == null)
            {
                var res = new ApiResponse
                { 
                    StatusCode = 404,
                    Message = "No user found"
                };
                return Task.FromResult(res);
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                var res = new ApiResponse
                {
                    Message = "Wrong password"
                };
                return Task.FromResult(res);
            }
            string token = CreateToken(user);
            var response = new ApiResponse
            {
                Message = "Login successful",
                Result = token
            };
            return Task.FromResult(response);   
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

        public Task<ApiResponse> Register(RegisterDto registerDto)
        {
            string passwordhash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var user = new User();
            user.Username = registerDto.Username;
            user.PasswordHash = passwordhash;
            user.Name = registerDto.Name;
            _repository.CreateEntity(user);
            return Task.FromResult(new ApiResponse
            {
                StatusCode = 200,
                Message = "User created"
            });
        }
    }
}
