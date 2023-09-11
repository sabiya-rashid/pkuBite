using AutoWrapper.Wrappers;
using Common.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAccountServices
    {
        Task<ApiResponse> Register(RegisterDto registerDto);
        Task<ApiResponse> Login(RegisterDto registerDto);
    }
}
