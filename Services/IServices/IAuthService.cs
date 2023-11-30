using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;

namespace Services.IServices
{
    public interface IAuthService
    {
        public Task<ApiResponse<Tokens>> Login(LoginDto model);
        public Task<ApiResponse<Tokens>> SignUp(SignUpDto model);
        public ApiResponse<string> Me(string email);
    }
}