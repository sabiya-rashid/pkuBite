using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Account
{
    public class RegisterDto
    {
        public string? Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
