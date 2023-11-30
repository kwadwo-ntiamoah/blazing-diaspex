using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet("me")]
        public ActionResult<ApiResponse<string>> Me() {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
            return _authService.Me(email);
        }

        [HttpPost("token")]
        public async Task<ActionResult<ApiResponse<Tokens>>> Login(LoginDto model) {
            return await _authService.Login(model);
        }

        [HttpPost("register/local")]
        public async Task<ActionResult<ApiResponse<Tokens>>> RegisterLocal(SignUpDto model) {
            return await _authService.SignUp(model);
        }
    }
}