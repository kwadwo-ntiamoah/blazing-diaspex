using Data.Models;
using Data.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JWTService
    {
        private readonly IConfiguration _config;

        public JWTService(IConfiguration config)
        {
            _config = config;
        }

        public ApiResponse<Tokens> GenerateToken(TokenDto model)
        {
            var response = new ApiResponse<Tokens>();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]!);
                var claims = new List<Claim>();

                foreach(var role in model.Roles)
                {
                    var claim = new Claim(ClaimTypes.Role, role);
                    claims.Add(claim);
                }

                // add Email and user Id to claims
                claims.Add(new Claim(ClaimTypes.Sid, model.UserId!));
                claims.Add(new Claim(ClaimTypes.Email, model.Email!));

                var descriptor = new SecurityTokenDescriptor
                {
                    Issuer = _config["JWT:Issuer"],
                    Audience = _config["JWT:Audience"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = handler.CreateToken(descriptor) ?? throw new Exception("An error occurred creating token");
                var tokens = new Tokens
                {
                    Token = handler.WriteToken(token)
                };

                response.Data = tokens;
                response.Status = HttpStatusCode.OK;

                return response;
            }
            catch (Exception)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "An error occurred. Try again later";

                return response;
            }
        }
    }
}
